using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MauiScanApp.Models;

namespace MauiScanApp.ViewModels
{
    public class CAttendViewModel
    {
        public CAttendViewModel()
        {
            App app = Application.Current as App;
            if (app != null)
            {
                if(app.attendLists != null)
                {
                    _attendList = app.attendLists;
                }
                else
                    _attendList = new List<CAttendList>();
            }
        }
        private List<CAttendList> _attendList = null; 
        public List<CAttendList> All
        {
            get { return _attendList; }
            set { _attendList = value; }
        }

        public int getAttendListCount()
        {
            return _attendList.Count;
        }

        public int getAttendedCount()
        {
            return _attendList.Where(x=>x.IsAttend==true).Count();
        }

        private async void loadData(int productDetailId)
        {
            // call api : 符合product detail的order detail
            //http://localhost:5016/AttendList?id=1

            HttpClient client = new HttpClient();
            Uri uri = new Uri($"http://10.0.2.2:5016/AttendList?id={productDetailId}");
            _attendList = await client.GetFromJsonAsync<List<CAttendList>>(uri);

            App app = Application.Current as App;
            if (app != null && app.attendLists == null)
            {
                app.attendLists = _attendList;
            }
                //CAttendList x = new CAttendList();
                //x.OrderDetailId = 1;
                //x.Name = "王秋夢";
                //x.Phone = "0928110112";
                //x.odNumber = "OD111111";
                //x.IsAttend = false;
                //_attendList.Add(x);

                //x = new CAttendList();
                //x.OrderDetailId = 2;
                //x.Name = "王可愛";
                //x.Phone = "0928110112";
                //x.odNumber = "OD222222";
                //x.IsAttend = true;
                //_attendList.Add(x);
            }
    }



}
