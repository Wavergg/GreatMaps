using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Net;
using System.Web.Script.Serialization;
using GMap.NET.ObjectModel;

namespace projectGreatMaps
{
    public partial class Form1 : Form
    {
        GMapOverlay markers = new GMapOverlay("markers");
        GMapOverlay polygons = new GMapOverlay("polygons");
        GMapOverlay polygonsCircle = new GMapOverlay("polygons");
        List<Position> hull;

        string url = @"http://developer.kensnz.com/getlocdata";

        List<Position> points = new List<Position>();
        Dictionary<string,int> users = new Dictionary<string,int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gmap.Position = new PointLatLng(-46.411300, 168.352520);
            gmap.MapProvider = BingMapProvider.Instance;
            gmap.CanDragMap = true;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.ShowCenter = false;
            gmap.ShowTileGridLines = false;
            gmap.MouseWheelZoomType = MouseWheelZoomType.ViewCenter;
            gmap.MouseWheelZoomEnabled = false;
            gmap.MinZoom = 2;
            gmap.MaxZoom = 18;
            gmap.MouseWheel += Gmap_MouseWheel;
        }

        private void GetGeolocationData()
        {
            markers = new GMapOverlay("markers");
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString(url);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var JSONArray
                 = ser.Deserialize<Dictionary<string, string>[]>(json);
                foreach (Dictionary<string, string> map in JSONArray)
                {
                    string userid = map["userid"];
                    double latitude = double.Parse(map["latitude"]);
                    double longitude = double.Parse(map["longitude"]);
                    string description = map["description"];
                    DateTime createdAt = Convert.ToDateTime(map["created_at"]);
                    if (createdAt.Year != 2020) continue;

                    if (textBoxUserID.Text == "")
                    {
                        if (Convert.ToInt32(userid) > 0 && Convert.ToInt32(userid) < 37 )
                        {

                            GMapMarker marker = new GMarkerGoogle(new PointLatLng(latitude, longitude),
                                (GMarkerGoogleType)Convert.ToInt32(userid)
                                );

                            marker.ToolTipText = description;
                            markers.Markers.Add(marker);
                            gmap.Overlays.Add(markers);
                            points.Add(new Position(latitude, longitude));
                        }
                    }
                    else if (textBoxUserID.Text == userid)
                    {
                        GMapMarker marker = new GMarkerGoogle(new PointLatLng(latitude, longitude),
                                GMarkerGoogleType.yellow_dot
                                );

                        marker.ToolTipText = description;
                        markers.Markers.Add(marker);
                        gmap.Overlays.Add(markers);
                        points.Add(new Position(latitude, longitude));
                    }
                }
            }
        }

        private void Gmap_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Delta > 0 && gmap.Zoom != 18)
            {
                gmap.Zoom += 1;
            } else if (e.Delta <0 &&gmap.Zoom != 2)
            {
                gmap.Zoom -= 1;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            gmap.Overlays.Clear();
            polygons.Clear();
            markers.Clear();
            points.Clear();
            polygonsCircle.Clear();
            GetGeolocationData();
        }

        private void buttonDrawHull_Click(object sender, EventArgs e)
        {
            points = points.OrderBy(pointX => pointX.YCoordinate).ThenBy(pointX => pointX.XCoordinate).ToList();

            OrderByExtremity();
            MapHull();
        }

        private void MapHull()
        {
            if (points.Count < 3) return;
            gmap.Overlays.Remove(polygons);
            polygons.Clear();
            polygons = new GMapOverlay("polygons");
            hull = new List<Position>();
            hull.Add(points[0]);
            hull.Add(points[1]);

            for(int i = 2; i < points.Count; i++)
            {
                hull.Add(points[i]);
                while(hull.Count > 2 && CalculateMetrix(hull[hull.Count-3],hull[hull.Count-2],hull[hull.Count-1]) <= 0)
                {
                    hull.RemoveAt(hull.Count - 2);
                }
            }

            
            List<PointLatLng> pointsMap = new List<PointLatLng>();

            foreach (Position p in hull)
            {
                pointsMap.Add(new PointLatLng(p.XCoordinate, p.YCoordinate));
                
            }
            GMapPolygon polygon = new GMapPolygon(pointsMap, "Convex Hull");
            polygons.Polygons.Add(polygon);
            gmap.Overlays.Add(polygons);
            gmap.Zoom = gmap.Zoom - 1;
            

        }

        private void OrderByExtremity()
        {
            for(int i = 1; i < points.Count; i++)
            {
                for(int j = i + 1; j < points.Count; j++)
                {
                    if(CalculateMetrix(points[0], points[i], points[j]) < 0)
                    {
                        Position temp = points[i];
                        points[i] = points[j];
                        points[j] = temp;
                    }
                }
            }
        }

        private double CalculateMetrix(Position a,Position b, Position c)
        {
            double result = (a.XCoordinate * b.YCoordinate - a.XCoordinate * c.YCoordinate) - (a.YCoordinate * b.XCoordinate - a.YCoordinate * c.XCoordinate) + (b.XCoordinate * c.YCoordinate - c.XCoordinate * b.YCoordinate);
            return result;
        }

        private void buttonEnclosingCircle_Click(object sender, EventArgs e)
        {
            
            Position a = null;
            Position b = null;

            Position c = null;

            double highestLength = 0;

            for (int i = 0; i < hull.Count; i++)
            {
                for (int j = i+1; j < hull.Count; j++)
                {
                    if (highestLength < GetLength(hull[i], hull[j]))
                    {
                        highestLength = GetLength(hull[i], hull[j]);
                        a = hull[i];
                        b = hull[j];
                    }
                }
            }
            
            double smallestAngle = 180;
            hull.Remove(a);
            hull.Remove(b);
            for (int i = 0; i < hull.Count; i++)
            {
                double currentAngle = GetAngleC(a, b, hull[i]);
                if(currentAngle != 0 && currentAngle < smallestAngle)
                {
                    smallestAngle = currentAngle;
                    c = hull[i];
                }
            }
            hull.Add(a);
            hull.Add(b);

            Position center;
            double radius;

            double angleA = GetAngleA(a, b, c, smallestAngle);
            double angleB = 180 - smallestAngle - angleA;

            GMapMarker markerA = new GMarkerGoogle(new PointLatLng(a.XCoordinate, a.YCoordinate),
                            GMarkerGoogleType.red_pushpin);
            markerA.ToolTipText = "A";
            markers.Markers.Add(markerA);
            gmap.Overlays.Add(markers);

            GMapMarker markerB = new GMarkerGoogle(new PointLatLng(b.XCoordinate, b.YCoordinate),
                            GMarkerGoogleType.purple_pushpin);
            markerB.ToolTipText = "B";
            markers.Markers.Add(markerB);
            gmap.Overlays.Add(markers);

            GMapMarker markerC = new GMarkerGoogle(new PointLatLng(c.XCoordinate, c.YCoordinate),
                            GMarkerGoogleType.yellow_pushpin);
            markerC.ToolTipText = "C";
            markers.Markers.Add(markerC);
            gmap.Overlays.Add(markers);

            //Check if the a,b,c point which makes a triangle is an obtuse or acute, and determine center and radius.
            if (smallestAngle > 90 || angleA > 90 || angleB > 90)
            {
                center = new Position((a.XCoordinate + b.XCoordinate ) / 2 , (a.YCoordinate + b.YCoordinate) / 2 );
                //get from one position to center to determine radius
                radius = GetLength(a, center);
            }
            else
            {
                b = new Position(b.XCoordinate - a.XCoordinate, b.YCoordinate - a.YCoordinate);
                c = new Position(c.XCoordinate - a.XCoordinate, c.YCoordinate - a.YCoordinate);

                double d = 2 * (b.XCoordinate * c.YCoordinate - b.YCoordinate * c.XCoordinate);
                double centerX = 1 / d * (c.YCoordinate * (b.XCoordinate * b.XCoordinate + b.YCoordinate * b.YCoordinate) - b.YCoordinate * (c.XCoordinate * c.XCoordinate + c.YCoordinate * c.YCoordinate));
                double centerY = 1 / d * (b.XCoordinate * (c.XCoordinate * c.XCoordinate + c.YCoordinate * c.YCoordinate) - c.XCoordinate * (b.XCoordinate * b.XCoordinate + b.YCoordinate * b.YCoordinate));
                center = new Position(centerX, centerY);
                radius = Math.Sqrt(Math.Pow(center.XCoordinate, 2) + Math.Pow(center.YCoordinate, 2)) *1.4;
                center = new Position(a.XCoordinate + center.XCoordinate , a.YCoordinate + center.YCoordinate );
            }

            GMapMarker marker = new GMarkerGoogle(new PointLatLng(center.XCoordinate, center.YCoordinate),
                            GMarkerGoogleType.blue_dot);
            marker.ToolTipText = "center";
            markers.Markers.Add(marker);
            gmap.Overlays.Add(markers);

            DrawCircle(center,radius);
        }

        private void DrawCircle(Position center, double radius)
        {
            int segments = 360;
           
            List<PointLatLng> gpollist = new List<PointLatLng>();
          
            double seg = Math.PI * 2 / segments;

            for (int i = 0; i < segments; i++)
            {
                double theta = seg * i;
                double a = center.XCoordinate + (Math.Cos(theta) * radius) /1.4;
                double b = center.YCoordinate + Math.Sin(theta) * radius;

                PointLatLng gpoi = new PointLatLng(a, b);

                gpollist.Add(gpoi);
            }
            GMapPolygon gpol = new GMapPolygon(gpollist, "pol");
            gpol.Fill = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
            polygonsCircle.Polygons.Add(gpol);
            
            gmap.Overlays.Add(polygonsCircle);
            gmap.Zoom = gmap.Zoom - 1;
        }


        private double GetAngleC(Position a, Position b, Position c)
        {
            double aLength = GetLength(b, c);
            double kLength = GetLength(a, b);
            double bLength = GetLength(a, c);

            double combineLength = Math.Pow(kLength,2) - ( Math.Pow(aLength, 2) + Math.Pow(bLength, 2));
            double cosC = combineLength / - (2 * (aLength * bLength));

            double angleC = Math.Acos(cosC) * 180 / Math.PI;
            return angleC;
        }

        private double GetAngleA(Position a, Position b, Position c, double angleC)
        {
            angleC = angleC / 180 * Math.PI;
            double aLength = GetLength(b,c);
            double kLength = GetLength(a,b);
            double sinA = (aLength * Math.Sin(angleC)) / kLength;
            double angleA = Math.Abs(Math.Asin(sinA) * 180 / Math.PI);
            return angleA;
        }

        private double GetLength(Position a, Position b)
        {
            a.XCoordinate = a.XCoordinate * 1.4;
            b.XCoordinate = b.XCoordinate * 1.4;
            double result = Math.Sqrt(Math.Pow(b.XCoordinate - a.XCoordinate, 2) + Math.Pow(b.YCoordinate - a.YCoordinate, 2));
            a.XCoordinate = a.XCoordinate / 1.4;
            b.XCoordinate = b.XCoordinate / 1.4;
            return result;
        }

        private void gmap_Resize(object sender, EventArgs e)
        {
            gmap.Width = this.Width;
            gmap.Height = this.Height;
        }
    }
}
