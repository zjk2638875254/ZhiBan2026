//using Bentley.DgnPlatform;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;
using Bentley.MstnPlatformNET.InteropServices;

using NPOI.HPSF;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class 面板_Bentley
    {
        static double meter_rate = Session.Instance.GetActiveDgnModel().GetModelInfo().UorPerMeter;

        private static bool make_shape(DPoint3d[] pos, ref ElementId id)
        {
            try
            {
                DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                ComplexShapeElement complex_shape = new ComplexShapeElement(dgnModel, null);
                for (int i = 0; i < pos.Length - 1; i++)
                {
                    DSegment3d seg = new DSegment3d(pos[i], pos[i + 1]);
                    LineElement line = new LineElement(dgnModel, null, seg);
                    complex_shape.AddComponentElement(line);
                }
                DSegment3d seg_last = new DSegment3d(pos[pos.Length - 1], pos[0]);
                LineElement line_last = new LineElement(dgnModel, null, seg_last);
                complex_shape.AddComponentElement(line_last);
                complex_shape.AddComponentComplete();
                complex_shape.AddToModel();
                id = complex_shape.ElementId;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool combine_surfance(ElementId[] surfance_list, ref ElementId res_id)
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
                for (int i = 0; i < surfance_list.Count(); i++)
                {
                    Element ele = dgnModel.FindElementById(surfance_list[i]);
                    SelectionSetManager.AddElement(ele, dgnModel);
                }
                Session.Instance.Keyin("CONSTRUCT STITCH");
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Bentley.Interop.MicroStationDGN.Element MSDgn_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
                res_id = (ElementId)MSDgn_ele.ID64;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool convert_to_solid(ElementId sur_id, ref ElementId solid_id)
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            Bentley.Interop.MicroStationDGN.Point3d P = new Bentley.Interop.MicroStationDGN.Point3d();
            P.X = 0.0;
            P.Y = 0.0;
            P.Z = 0.0;
            ElementAgenda agenda = new ElementAgenda();
            SelectionSetManager.BuildAgenda(ref agenda);
            SelectionSetManager.EmptyAll();
            Element ele = dgnModel.FindElementById(sur_id);
            SelectionSetManager.AddElement(ele, dgnModel);

            //CadInputQueue.SendCommand "CONVERT TO SOLID"
            //SetCExpressionValue "tcb->ms3DToolSettings.convert.convertToSolid", 1, "SOLIDMODELING"
            Session.Instance.Keyin("CONVERT TO SOLID");
            Utilities.ComApp.SetCExpressionValue("tcb->ms3DToolSettings.convert.convertToSolid", 1, "SOLIDMODELING");

            Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
            Bentley.Interop.MicroStationDGN.Element MSDgn_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
            solid_id = (ElementId)MSDgn_ele.ID64;
            return true;
        }

        public static void make_solid(ArrayList pos)
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            long default_int_id = 0;
            ArrayList id_list = new ArrayList();
            for (int i = 0; i < pos.Count; i++)
            {
                point[] p_list1 = (point[])pos[i];
                point[] p_list2 = (point[])pos[i + 1];
                if (i == pos.Count)
                {
                    p_list1 = (point[])pos[i];
                    p_list2 = (point[])pos[0];
                }
                DPoint3d p1 = new DPoint3d(p_list1[0].x * meter_rate, p_list1[0].y * meter_rate, p_list1[0].z * meter_rate);
                DPoint3d p2 = new DPoint3d(p_list1[1].x * meter_rate, p_list1[1].y * meter_rate, p_list1[1].z * meter_rate);
                DPoint3d p3 = new DPoint3d(p_list2[0].x * meter_rate, p_list2[0].y * meter_rate, p_list2[0].z * meter_rate);
                DPoint3d p4 = new DPoint3d(p_list2[1].x * meter_rate, p_list2[1].y * meter_rate, p_list2[1].z * meter_rate);
                DPoint3d[] shape_p = new DPoint3d[4] { p1, p2, p4, p3 };
                
                ElementId shape_id = (ElementId)default_int_id;
                if(make_shape(shape_p, ref shape_id))
                {
                    id_list.Add(shape_id);
                }
                else
                {
                    MessageBox.Show("构造面板错误");
                    return;
                }
            }

            DPoint3d[] shape_p_high = new DPoint3d[pos.Count];
            for (int i = 0; i < pos.Count; i++)
            {
                point[] p_list = (point[])pos[i];
                DPoint3d p = new DPoint3d(p_list[0].x * meter_rate, p_list[0].y * meter_rate, p_list[0].z * meter_rate);
                shape_p_high[i] = p;
            }
            ElementId shape_id_high = (ElementId)default_int_id;
            if (make_shape(shape_p_high, ref shape_id_high))
            {
                id_list.Add(shape_id_high);
            }
            else
            {
                MessageBox.Show("构造面板错误");
                return;
            }

            DPoint3d[] shape_p_low = new DPoint3d[pos.Count];
            for (int i = 0; i < pos.Count; i++)
            {
                point[] p_list = (point[])pos[i];
                DPoint3d p = new DPoint3d(p_list[1].x * meter_rate, p_list[1].y * meter_rate, p_list[1].z * meter_rate);
                shape_p_low[i] = p;
            }
            ElementId shape_id_low = (ElementId)default_int_id;
            if (make_shape(shape_p_low, ref shape_id_low))
            {
                id_list.Add(shape_id_low);
            }
            else
            {
                MessageBox.Show("构造面板错误");
                return;
            }
            ElementId shape_all = (ElementId)default_int_id;
            ElementId[] shape_id_list = new ElementId[id_list.Count];
            for (int i = 0; i < id_list.Count; i++)
                shape_id_list[i] = (ElementId)id_list[i];
            combine_surfance(shape_id_list, ref shape_all);
            ElementId solid_id = (ElementId)default_int_id;
            convert_to_solid(shape_all, ref solid_id);
        }

        public static void test_high(ArrayList points)
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            for (int i = 0; i < points.Count; i++)
            {
                point[] pos = (point[])points[i];
                DPoint3d p1 = new DPoint3d(pos[0].x * meter_rate, pos[0].y * meter_rate, pos[0].z * meter_rate);
                DPoint3d p2 = new DPoint3d(pos[1].x * meter_rate, pos[1].y * meter_rate, pos[1].z * meter_rate);
                DSegment3d seg = new DSegment3d(p1, p2);
                LineElement line = new LineElement(dgnModel, null, seg);
                line.AddToModel();
            }
        }

        public static void test_mianban(ArrayList points)
        {
            DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
            for (int i = 0; i < points.Count; i++)
            {
                point[] pos = (point[])points[i];
                DPoint3d p1 = new DPoint3d(pos[0].x * meter_rate, pos[0].y * meter_rate, pos[0].z * meter_rate);
                DPoint3d p2 = new DPoint3d(pos[1].x * meter_rate, pos[1].y * meter_rate, pos[1].z * meter_rate);
                DSegment3d seg1 = new DSegment3d(p1, p2);
                LineElement line1 = new LineElement(dgnModel, null, seg1);
                line1.AddToModel();

                p1 = new DPoint3d(pos[1].x * meter_rate, pos[1].y * meter_rate, pos[1].z * meter_rate);
                p2 = new DPoint3d(pos[2].x * meter_rate, pos[2].y * meter_rate, pos[2].z * meter_rate);
                DSegment3d seg2 = new DSegment3d(p1, p2);
                LineElement line2 = new LineElement(dgnModel, null, seg2);
                line2.AddToModel();

                p1 = new DPoint3d(pos[2].x * meter_rate, pos[2].y * meter_rate, pos[2].z * meter_rate);
                p2 = new DPoint3d(pos[3].x * meter_rate, pos[3].y * meter_rate, pos[3].z * meter_rate);
                DSegment3d seg3 = new DSegment3d(p1, p2);
                LineElement line3 = new LineElement(dgnModel, null, seg3);
                line3.AddToModel();

                p1 = new DPoint3d(pos[3].x * meter_rate, pos[3].y * meter_rate, pos[3].z * meter_rate);
                p2 = new DPoint3d(pos[0].x * meter_rate, pos[0].y * meter_rate, pos[0].z * meter_rate);
                DSegment3d seg4 = new DSegment3d(p1, p2);
                LineElement line4 = new LineElement(dgnModel, null, seg4);
                line4.AddToModel();
            }
        }
    }
}
