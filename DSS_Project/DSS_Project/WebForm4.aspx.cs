﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DSS_Project
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["LocalMySql"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton1.Visible = false;
            Button1.Visible = true;
            txtDSSWoman.Visible = true; 
            txtName.Visible = true;
            txtName.Focus();

            if (Session["MW_DSS_ID"] != null)
            {
                Button1.Visible = false;
                txtDSSWoman.Visible = false;
                txtName.Visible = false;
                ImageButton1.Visible = true;
                ShowData();
            }
            else if (Session["txtWomanName"] != null || Session["txtDSSWoman"] != null)
            {
                txtDSSWoman.Text = Convert.ToString(Session["txtDSSWoman"]);
                txtName.Text = Convert.ToString(Session["txtWomanName"]);
                ShowData();
                Session["txtWomanName"] = null;
                Session["txtDSSWoman"] = null;
            }
            else { ShowData(); }
            Session["FM_DSS_ID"] = null;
            Session["DSS_Member"] = null;
             
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowData();

        }

        private void ShowData()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(constr);
                if (Session["MW_DSS_ID"] != null)
                {
                    lbe_Mother.Text = Convert.ToString(Session["MW_DSS_ID"]);

                    MySqlCommand cmd = new MySqlCommand("select * from family_members where member_type='mw' and dss_id_member='"+Convert.ToString(Session["MW_DSS_ID"])+"' order by dss_id_member");
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            {
                                sda.Fill(dt);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                    }

                }
                else if (txtName.Text != "" && txtDSSWoman.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("select * from family_members where member_type='mw' and name like '%" + txtName.Text + "%' and dss_id_member like '%"+txtDSSWoman.Text+"%' order by dss_id_member");
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            {
                                sda.Fill(dt);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
                else if (txtName.Text == "" && txtDSSWoman.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("select * from family_members where member_type='mw' and dss_id_member like '%" + txtDSSWoman.Text+ "%' order by dss_id_member");
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            {
                                sda.Fill(dt);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
                else if (txtName.Text != "" && txtDSSWoman.Text == "")
                {
                    MySqlCommand cmd = new MySqlCommand("select * from family_members where member_type='mw' and name like '%"+txtName.Text+"%' order by dss_id_member");
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            {
                                sda.Fill(dt);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("select * from family_members where member_type='mw' order by dss_id_member");
                    {
                        MySqlDataAdapter sda = new MySqlDataAdapter();
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            {
                                sda.Fill(dt);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script type=\"text/javascript\">alert('" + ex.Message + "')</script>");
            }
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
                Response.Redirect("WebForm5.aspx");
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ShowData();
        }
        protected void Link_Button1(object sender, EventArgs e)
        {
            Session["txtWomanName"] = txtName.Text;
            Session["txtDSSWoman"] = txtDSSWoman.Text;
            string dss_id_member = ((LinkButton)sender).Text;
            Session["dss_id_member"] = dss_id_member;
            Session["WebPage3"] = "WebForm4";
            Session["DSS_Member"] = dss_id_member;
            Response.Redirect("WebForm3.aspx");
        }
        protected void Link_Button2(object sender, EventArgs e)
        {
            Session["txtWomanName"] = txtName.Text;
            Session["txtDSSWoman"] = txtDSSWoman.Text;
            Session["WebPage2"] = "WebForm4";
            string Dss_ID_hh = ((LinkButton)sender).Text;
            Session["FM_DSS_ID"] = Dss_ID_hh;
            Response.Redirect("WebForm2.aspx");
        }
    }
}