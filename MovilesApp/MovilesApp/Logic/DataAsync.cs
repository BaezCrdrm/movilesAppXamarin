using MovilesApp.Model;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MovilesApp.Logic
{
    public class DataAsync
    {
        public Task<string> Get(Event ev)
        {
            string line = "";
            int response = 0;
            StringBuilder result = null;

            try
            {
                string strUrl = String.Format("http://livebr.esy.es/scripts/service/rest/req_events.php?evId={0}&tpId=",
                    ev.Id);
                if (ev.Type.Id > 0)
                    strUrl += ev.Type.Id.ToString();

                
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }
    }
}
