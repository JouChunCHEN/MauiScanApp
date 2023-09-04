using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MauiScanApp.Models;
using Microsoft.Extensions.Logging;

namespace MauiScanApp.ViewModels
{
    public class CEventListViewModel
    {
        public CEventListViewModel()
        {
            App app = Application.Current as App;
            if (app != null)
            {
                _events = app.eventLists;
            }
        }
        private List<CEvent> _events = null;

        public List<CEvent> All
        {
            get { return _events; }
            set { _events = value; }
        }

        public CEvent getEvent(int id)
        {
            CEvent e = _events.Where(p=>p.ProductDetail_ID == id).FirstOrDefault();
            return e;
        }

        public List<CEvent> getNewEevents()
        {
            List<CEvent> newEvents = new List<CEvent>();
            foreach (CEvent x in _events)
            {
                if (x.BeginTime >= DateTime.Now)
                {
                    newEvents.Add(x);
                }
            }
            return newEvents.OrderBy(x => x.BeginTime).ToList();
        }

        public List<CEvent> getOldEevents()
        {
            List<CEvent> oldEvents = new List<CEvent>();
            foreach (CEvent x in _events)
            {
                if (x.BeginTime < DateTime.Now)
                {
                    oldEvents.Add(x);
                }
            }
            return oldEvents.OrderByDescending(x=>x.BeginTime).ToList();
        }
    }
}
