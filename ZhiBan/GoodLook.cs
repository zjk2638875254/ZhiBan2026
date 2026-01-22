using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;
using Bentley.MstnPlatformNET.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class GoodLook
    {
        static double meter_rate = Session.Instance.GetActiveDgnModel().GetModelInfo().UorPerMeter;

        public static void look_dam(point start, point end, double rate)
        {
            double Lxy = (start.z - 780) * rate;
            point dir = new point(end.x - start.x, end.y - start.y, 0);
            double mod = Math.Sqrt(dir.x * dir.x + dir.y * dir.y);
            point dir1 = new point(-1 * dir.y / mod, dir.x / mod, 0);
            point dir2 = new point(dir.y / mod, -1 * dir.x / mod, 0);
            point start1 = new point(start.x + dir1.x * Lxy, start.y + dir1.y * Lxy, 0);
            point end1 = new point(end.x + dir1.x * Lxy, end.y + dir1.y * Lxy, 0);
            point start2 = new point(start.x + dir2.x * Lxy, start.y + dir2.y * Lxy, 0);
            point end2 = new point(end.x + dir2.x * Lxy, end.y + dir2.y * Lxy, 0);

            long default_high = 0, default_low1 = 0, default_low2 = 0;
            ElementId id_high = (ElementId)default_high;
            ElementId id_low1 = (ElementId)default_low1;
            ElementId id_low2 = (ElementId)default_low2;
            make_line(start, end, ref id_high);
            make_line(start1, end1, ref id_low1);
            make_line(start2, end2, ref id_low2);
            ElementId[] sur1 = new ElementId[2] { id_high, id_low1 };
            ElementId[] sur2 = new ElementId[2] { id_high, id_low2 };
            make_surfnce(sur1);
            make_surfnce(sur2);
        }

        private static bool make_line(point p1, point p2, ref ElementId id)
        {
            try
            {
                DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                DPoint3d ps = new DPoint3d(p1.x * meter_rate, p1.y * meter_rate, p1.z * meter_rate);
                DPoint3d pe = new DPoint3d(p2.x * meter_rate, p2.y * meter_rate, p2.z * meter_rate);
                DSegment3d segment = new DSegment3d(ps, pe);
                LineElement line = new LineElement(dgnModel, null, segment);
                line.AddToModel();
                id = line.ElementId;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool make_surfnce(ElementId[] line_list)
        {
            try
            {
                DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                Bentley.Interop.MicroStationDGN.Point3d P = new Bentley.Interop.MicroStationDGN.Point3d();
                P.X = 0.0;
                P.Y = 0.0;
                P.Z = 0.0;
                ElementAgenda agenda = new ElementAgenda();
                SelectionSetManager.BuildAgenda(ref agenda);
                SelectionSetManager.EmptyAll();
                Element ele1 = dgnModel.FindElementById(line_list[0]);
                Element ele2 = dgnModel.FindElementById(line_list[1]);
                SelectionSetManager.AddElement(ele1, dgnModel);
                SelectionSetManager.AddElement(ele2, dgnModel);
                Session.Instance.Keyin("CONSTRUCT SURFACE EDGE");
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Bentley.Interop.MicroStationDGN.Element MSDgn_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

    }
}
