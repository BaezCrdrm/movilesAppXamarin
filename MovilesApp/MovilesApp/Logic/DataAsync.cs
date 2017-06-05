using MovilesApp.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Diagnostics;

namespace MovilesApp.Logic
{
    public class DataAsync
    {
        public async Task<string> GetAsync(Event ev)
        {
            HttpStatusCode response;
            string result = "";

            try
            {
                string strUrl = String.Format("http://livebr.esy.es/scripts/service/rest/req_events.php?evId={0}&tpId=",
                    ev.Id);
                if (ev.Type.Id > 0)
                    strUrl += ev.Type.Id.ToString();

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient con = new HttpClient(handler);
                var resp = await con.GetAsync(strUrl);
                Debug.WriteLine(resp.ToString());
                
                try
                {                   
                    response = resp.StatusCode;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Ha habido un error al establecer la conexión");
                    Debug.WriteLine(ex.Message);
                    return null;
                }

                if (response == HttpStatusCode.OK)
                {
                    result = await resp.Content.ReadAsStringAsync();
                    return result;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }    
}
