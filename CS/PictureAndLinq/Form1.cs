using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;

namespace PictureAndLinq {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;

            AdventureWorksDataContext dc = new AdventureWorksDataContext();
            BindingSource bs = new BindingSource(dc.ProductPhotos, "");
            gridControl1.DataSource = bs;
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e) {
            if(e.Column.FieldName == "LargePhoto_Unbound") {
                GridView view = (GridView)sender;
                IList dataSource = (IList)view.DataSource;
                ProductPhoto pf = (ProductPhoto)dataSource[e.ListSourceRowIndex];
                if(pf == null) return;
                if(e.IsGetData) {
                    e.Value = pf.LargePhoto.ToArray();
                }
                else if(e.IsSetData) {
                    byte[] data = (byte[])e.Value;
                    pf.LargePhoto = new System.Data.Linq.Binary(data);
                }
            }
        }
    }
}
