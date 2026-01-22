using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;
using Bentley.MstnPlatformNET.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class BentleyGeo
    {
        static double meter_rate = Session.Instance.GetActiveDgnModel().GetModelInfo().UorPerMeter;

        public static void make_geo_single(ArrayList points_list)
        {
            try
            {
                ArrayList ids = new ArrayList();

                for (int j = 0; j < points_list.Count / 14; j++)
                {
                    #region 构造平行直线和面
                    //ABCDTQX
                    long default_int = 0;
                    ElementId[] line_id = new ElementId[7];
                    for (int t = 0; t < line_id.Length; t++)
                    {
                        ElementId id = (ElementId)default_int;
                        make_line((point)points_list[j * 14 + t], (point)points_list[j * 14 + 7 + t], ref id);
                        line_id[t] = id;
                    }
                    #endregion
                    ElementId[] line_list1 = new ElementId[4] { line_id[0], line_id[3], line_id[5], line_id[6] };
                    ElementId[] line_list2 = new ElementId[5] { line_id[5], line_id[6], line_id[1], line_id[2], line_id[4] };

                    #region 构造垂直剖面
                    point[] left1 = new point[4] { (point)points_list[j * 14], (point)points_list[j * 14 + 3], (point)points_list[j * 14 + 5], (point)points_list[j * 14 + 6] };
                    point[] left2 = new point[5] { (point)points_list[j * 14 + 5], (point)points_list[j * 14 + 6], (point)points_list[j * 14 + 1], (point)points_list[j * 14 + 2], (point)points_list[j * 14 + 4] };
                    point[] right1 = new point[4] { (point)points_list[j * 14 + 7], (point)points_list[j * 14 + 7 + 3], (point)points_list[j * 14 + 7 + 5], (point)points_list[j * 14 + 7 + 6] };
                    point[] right2 = new point[5] { (point)points_list[j * 14 + 7 + 5], (point)points_list[j * 14 + 7 + 6], (point)points_list[j * 14 + 7 + 1], (point)points_list[j * 14 + 7 + 2], (point)points_list[j * 14 + 7 + 4] };
                    ElementId id_left1 = (ElementId)default_int;
                    ElementId id_left2 = (ElementId)default_int;
                    ElementId id_right1 = (ElementId)default_int;
                    ElementId id_right2 = (ElementId)default_int;
                    make_lines_C(left1, ref id_left1);
                    make_lines_C(left2, ref id_left2);
                    make_lines_C(right1, ref id_right1);
                    make_lines_C(right2, ref id_right2);
                    #endregion

                    ElementId[] res_list1 = new ElementId[line_list1.Length];
                    make_surfnce(line_list1, ref res_list1);
                    ElementId[] sur_list_all1 = new ElementId[res_list1.Length + 2];
                    for (int t = 0; t < res_list1.Length; t++)
                        sur_list_all1[t] = res_list1[t];
                    sur_list_all1[res_list1.Length] = id_left1;
                    sur_list_all1[res_list1.Length + 1] = id_right1;
                    long default_int_surfance1 = 0;
                    ElementId surfance_id1 = (ElementId)default_int_surfance1;
                    combine_surfance(sur_list_all1, ref surfance_id1);
                    long default_int_solid1 = 0;
                    ElementId solid_id1 = (ElementId)default_int_solid1;
                    convert_to_solid(surfance_id1, ref solid_id1);

                    ElementId[] res_list2 = new ElementId[line_list2.Length];
                    make_surfnce(line_list2, ref res_list2);
                    ElementId[] sur_list_all2 = new ElementId[res_list2.Length + 2];
                    for (int t = 0; t < res_list2.Length; t++)
                        sur_list_all2[t] = res_list2[t];
                    sur_list_all2[res_list2.Length] = id_left2;
                    sur_list_all2[res_list2.Length + 1] = id_right2;
                    long default_int_surfance2 = 0;
                    ElementId surfance_id2 = (ElementId)default_int_surfance2;
                    combine_surfance(sur_list_all2, ref surfance_id2);
                    long default_int_solid2 = 0;
                    ElementId solid_id2 = (ElementId)default_int_solid2;
                    convert_to_solid(surfance_id2, ref solid_id2);

                    DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                    Element ele1 = dgnModel.FindElementById(solid_id1);
                    Element ele2 = dgnModel.FindElementById(solid_id2);
                    ids.Add(ele1.ElementId);
                    ids.Add(ele2.ElementId);
                }
                long default_all_id = 0;
                ElementId all_id = (ElementId)default_all_id;
                //combine_solid(ids, ref all_id);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        public static void make_geo(ArrayList points_list)
        {
            try
            {
                bool regular = true;
                ArrayList ids = new ArrayList();
                for (int i = 0; i < points_list.Count; i++)
                {
                    ArrayList independent_sec = (ArrayList)points_list[i];
                    for (int j = 0; j < independent_sec.Count / 14; j++)
                    {
                        #region 构造平行直线和面
                        //ABCDTQX
                        long default_int = 0;
                        ElementId[] line_id = new ElementId[7];
                        for (int t = 0; t < line_id.Length; t++)
                        {
                            ElementId id = (ElementId)default_int;
                            make_line((point)independent_sec[j * 14 + t], (point)independent_sec[j * 14 + 7 + t], ref id);
                            line_id[t] = id;
                        }
                        #endregion
                        ElementId[] line_list1 = new ElementId[4] { line_id[0], line_id[3], line_id[5], line_id[6] };
                        ElementId[] line_list2 = new ElementId[5] { line_id[5], line_id[6], line_id[1], line_id[2], line_id[4] };
                        
                        #region 构造垂直剖面
                        point[] left1 = new point[4] { (point)independent_sec[j * 14], (point)independent_sec[j * 14 + 3], (point)independent_sec[j * 14 + 5], (point)independent_sec[j * 14 + 6] };
                        point[] left2 = new point[5] { (point)independent_sec[j * 14 + 5], (point)independent_sec[j * 14 + 6], (point)independent_sec[j * 14 + 1], (point)independent_sec[j * 14 + 2], (point)independent_sec[j * 14 + 4] };
                        point[] right1 = new point[4] { (point)independent_sec[j * 14 + 7], (point)independent_sec[j * 14 + 7 + 3], (point)independent_sec[j * 14 + 7 + 5], (point)independent_sec[j * 14 + 7 + 6] };
                        point[] right2 = new point[5] { (point)independent_sec[j * 14 + 7 + 5], (point)independent_sec[j * 14 + 7 + 6], (point)independent_sec[j * 14 + 7 + 1], (point)independent_sec[j * 14 + 7 + 2], (point)independent_sec[j * 14 + 7 + 4] };
                        ElementId id_left1 = (ElementId)default_int;
                        ElementId id_left2 = (ElementId)default_int;
                        ElementId id_right1 = (ElementId)default_int;
                        ElementId id_right2 = (ElementId)default_int;
                        make_lines_C(left1, ref id_left1);
                        make_lines_C(left2, ref id_left2);
                        make_lines_C(right1, ref id_right1);
                        make_lines_C(right2, ref id_right2);
                        #endregion

                        ElementId[] res_list1 = new ElementId[line_list1.Length];
                        make_surfnce(line_list1, ref res_list1);
                        ElementId[] sur_list_all1 = new ElementId[res_list1.Length + 2];
                        for (int t = 0; t < res_list1.Length; t++)
                            sur_list_all1[t] = res_list1[t];
                        sur_list_all1[res_list1.Length] = id_left1;
                        sur_list_all1[res_list1.Length + 1] = id_right1;
                        long default_int_surfance1 = 0;
                        ElementId surfance_id1 = (ElementId)default_int_surfance1;
                        combine_surfance(sur_list_all1, ref surfance_id1);
                        long default_int_solid1 = 0;
                        ElementId solid_id1 = (ElementId)default_int_solid1;
                        convert_to_solid(surfance_id1, ref solid_id1);

                        ElementId[] res_list2 = new ElementId[line_list2.Length];
                        make_surfnce(line_list2, ref res_list2);
                        ElementId[] sur_list_all2 = new ElementId[res_list2.Length + 2];
                        for (int t = 0; t < res_list2.Length; t++)
                            sur_list_all2[t] = res_list2[t];
                        sur_list_all2[res_list2.Length] = id_left2;
                        sur_list_all2[res_list2.Length + 1] = id_right2;
                        long default_int_surfance2 = 0;
                        ElementId surfance_id2 = (ElementId)default_int_surfance2;
                        combine_surfance(sur_list_all2, ref surfance_id2);
                        long default_int_solid2 = 0;
                        ElementId solid_id2 = (ElementId)default_int_solid2;
                        convert_to_solid(surfance_id2, ref solid_id2);

                        DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                        Element ele1 = dgnModel.FindElementById(solid_id1);
                        Element ele2 = dgnModel.FindElementById(solid_id2);
                        ids.Add(ele1.ElementId);
                        ids.Add(ele2.ElementId);
                        if (regular)
                        {
                            revise_color(ele1, 1);
                            revise_color(ele2, 1);
                        }
                        else
                        {
                            revise_color(ele1, 2);
                            revise_color(ele2, 2);
                        }
                    }
                    regular = !regular;
                }
                long default_all_id = 0;
                ElementId all_id = (ElementId)default_all_id;
                //combine_solid(ids, ref all_id);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        public static void make_geo_all(ArrayList points_list)
        {
            try
            {
                long default_int = 0;
                ArrayList ids = new ArrayList();
                for (int i = 0; i < points_list.Count; i++)
                {
                    ArrayList independent_sec = (ArrayList)points_list[i];
                    for (int j = 0; j < independent_sec.Count / 14; j++)
                    {
                        #region 构造平行直线和面
                        //ABCDTQX
                        ElementId[] line_id = new ElementId[7];
                        for (int t = 0; t < line_id.Length; t++)
                        {
                            ElementId id = (ElementId)default_int;
                            make_line((point)independent_sec[j * 14 + t], (point)independent_sec[j * 14 + 7 + t], ref id);
                            line_id[t] = id;
                        }
                        #endregion

                        #region 构造表面
                        ElementId[] line_list1 = new ElementId[4] { line_id[6], line_id[0], line_id[3], line_id[5] };
                        ElementId[] line_list2 = new ElementId[5] { line_id[6], line_id[1], line_id[2], line_id[4], line_id[5] };

                        ElementId[] res_list1 = new ElementId[line_list1.Length];
                        make_surfnce(line_list1, ref res_list1);
                        ElementId[] sur_list_all1 = new ElementId[res_list1.Length];
                        for (int t = 0; t < res_list1.Length; t++)
                            sur_list_all1[t] = res_list1[t];
                        long default_int_surfance1 = 0;
                        ElementId surfance_id1 = (ElementId)default_int_surfance1;
                        combine_surfance(sur_list_all1, ref surfance_id1);

                        ElementId[] res_list2 = new ElementId[line_list2.Length];
                        make_surfnce(line_list2, ref res_list2);
                        ElementId[] sur_list_all2 = new ElementId[res_list2.Length];
                        for (int t = 0; t < res_list2.Length; t++)
                            sur_list_all2[t] = res_list2[t];

                        long default_int_surfance2 = 0;
                        ElementId surfance_id2 = (ElementId)default_int_surfance2;
                        combine_surfance(sur_list_all2, ref surfance_id2);
                        #endregion
                        ids.Add(surfance_id1);
                        ids.Add(surfance_id2);
                    }
                }
                ElementId surfance_all = (ElementId)default_int;
                ElementId[] id_s = new ElementId[ids.Count];
                for (int i = 0; i < ids.Count; i++)
                    id_s[i] = (ElementId)ids[i];
                combine_surfance(id_s, ref surfance_all);
                ElementId solid_all = (ElementId)default_int;
                combine_solid(ids, ref solid_all);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private static bool make_lines_C(point[] p_list, ref ElementId id)
        {
            try
            {
                DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                DPoint3d[] pos = new DPoint3d[p_list.Length];
                for (int i = 0; i < p_list.Length; i++)
                    pos[i] = new DPoint3d(p_list[i].x * meter_rate, p_list[i].y * meter_rate, p_list[i].z * meter_rate);
                ShapeElement shape = new ShapeElement(dgnModel, null, pos);
                shape.AddToModel();
                id = shape.ElementId;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool make_surfnce(ElementId[] line_list, ref ElementId[] res_id)
        {
            try
            {
                res_id = new ElementId[line_list.Count()];
                DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                Bentley.Interop.MicroStationDGN.Point3d P = new Bentley.Interop.MicroStationDGN.Point3d();
                P.X = 0.0;
                P.Y = 0.0;
                P.Z = 0.0;
                ElementAgenda agenda = new ElementAgenda();
                SelectionSetManager.BuildAgenda(ref agenda);
                for (int i = 0; i < line_list.Count() - 1; i++)
                {
                    SelectionSetManager.EmptyAll();
                    Element line1 = dgnModel.FindElementById(line_list[i]);
                    Element line2 = dgnModel.FindElementById(line_list[i + 1]);
                    SelectionSetManager.AddElement(line1, dgnModel);
                    SelectionSetManager.AddElement(line2, dgnModel);
                    Session.Instance.Keyin("CONSTRUCT SURFACE EDGE");
                    Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                    Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);

                    Bentley.Interop.MicroStationDGN.Element surfance_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
                    res_id[i] = (ElementId)surfance_ele.ID64;
                }
                SelectionSetManager.EmptyAll();
                Element ele1 = dgnModel.FindElementById(line_list[line_list.Count() - 1]);
                Element ele2 = dgnModel.FindElementById(line_list[0]);
                SelectionSetManager.AddElement(ele1, dgnModel);
                SelectionSetManager.AddElement(ele2, dgnModel);
                Session.Instance.Keyin("CONSTRUCT SURFACE EDGE");
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Bentley.Interop.MicroStationDGN.Element MSDgn_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
                res_id[line_list.Count() - 1] = (ElementId)MSDgn_ele.ID64;
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

        private static bool revise_color(Element ele, uint color_num)
        {
            try
            {
                Element oldEle = Element.GetFromElementRef(ele.GetNativeElementRef());
                ElementPropertiesSetter elePropSet = new ElementPropertiesSetter();
                elePropSet.SetColor(color_num);
                elePropSet.Apply(ele);
                ele.ReplaceInModel(oldEle);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool combine_solid(ArrayList ids, ref ElementId solid_id)
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
                for (int i = 0; i < ids.Count; i++)
                {
                    ElementId ele_id = (ElementId)ids[i];
                    Element ele = dgnModel.FindElementById(ele_id);
                    SelectionSetManager.AddElement(ele, dgnModel);
                }
                Session.Instance.Keyin("CONSTRUCT UNION");
                Utilities.ComApp.SetCExpressionValue("s_smartFeatureSettings.m_boolean.m_associateToParametrics", 0, "SMARTFEATURE");
                Utilities.ComApp.SetCExpressionValue("s_smartFeatureSettings.m_boolean.m_uniteLeafMode", 0, "SMARTFEATURE");
                Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                Bentley.Interop.MicroStationDGN.Element MSDgn_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
                solid_id = (ElementId)MSDgn_ele.ID64;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region 一体化生成---作废
        private static bool make_surfnce_all(ElementId[] line_list, ref ElementId[] res_id)
        {
            try
            {
                res_id = new ElementId[line_list.Count()];
                DgnModel dgnModel = Session.Instance.GetActiveDgnModel();
                Bentley.Interop.MicroStationDGN.Point3d P = new Bentley.Interop.MicroStationDGN.Point3d();
                P.X = 0.0;
                P.Y = 0.0;
                P.Z = 0.0;
                ElementAgenda agenda = new ElementAgenda();
                SelectionSetManager.BuildAgenda(ref agenda);
                for (int i = 0; i < line_list.Count() - 1; i++)
                {
                    SelectionSetManager.EmptyAll();
                    Element line1 = dgnModel.FindElementById(line_list[i]);
                    Element line2 = dgnModel.FindElementById(line_list[i + 1]);
                    SelectionSetManager.AddElement(line1, dgnModel);
                    SelectionSetManager.AddElement(line2, dgnModel);
                    Session.Instance.Keyin("CONSTRUCT SURFACE EDGE");
                    Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);
                    Utilities.ComApp.CadInputQueue.SendDataPoint(ref P, Utilities.ComApp.ActiveDesignFile.Views[1], 0);

                    Bentley.Interop.MicroStationDGN.Element surfance_ele = Utilities.ComApp.ActiveModelReference.GetLastValidGraphicalElement();
                    res_id[i] = (ElementId)surfance_ele.ID64;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        #endregion


        #region 以下是构造切分面
        public static bool make_cut_surfance(point base_points, double[] v1, double[] v2)
        {
            try
            {
                long default_id = 0;
                point p1 = new point(base_points.x + v1[0] + v2[0], base_points.y + v1[1] + v2[1], base_points.z + v1[2] + v2[2]);
                point p2 = new point(base_points.x + v1[0] - v2[0], base_points.y + v1[1] - v2[1], base_points.z + v1[2] - v2[2]);
                point p3 = new point(base_points.x - v1[0] + v2[0], base_points.y - v1[1] + v2[1], base_points.z - v1[2] + v2[2]);
                point p4 = new point(base_points.x - v1[0] - v2[0], base_points.y - v1[1] - v2[1], base_points.z - v1[2] - v2[2]);
                ElementId line1_id = (ElementId)default_id;
                ElementId line2_id = (ElementId)default_id;
                ElementId sur_id = (ElementId)default_id;
                BentleyGeo.point_to_line(p1, p2, ref line1_id);
                BentleyGeo.point_to_line(p3, p4, ref line2_id);
                BentleyGeo.line_to_surfance(line1_id, line2_id, ref sur_id);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool point_to_line(point p1, point p2, ref ElementId id)
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

        private static bool line_to_surfance(ElementId line1, ElementId line2, ref ElementId res_id)
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
                Element ele1 = dgnModel.FindElementById(line1);
                Element ele2 = dgnModel.FindElementById(line2);
                SelectionSetManager.AddElement(ele1, dgnModel);
                SelectionSetManager.AddElement(ele2, dgnModel);
                Session.Instance.Keyin("CONSTRUCT SURFACE EDGE");
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
        #endregion
    }
}
