using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageswap.Models
{
    public class ImageVO
    {
        public string Code { set; get; }
        public string Source { set; get; }
        public string Destination { set; get; }
        public string Url { set; get; }
        public DateTime Creation { set; get; }
        public int Hours { set; get; }
        public DateTime Deadline { set; get; }
        public List<RequestVO> Requests { set; get; }
        public ImageVO()
        {
            this.Requests = new List<RequestVO>();
        }

        public void AddRequest(string address)
        {
            if (this.Requests == null)
                this.Requests = new List<RequestVO>();
            RequestVO request = new RequestVO();
            request.Address = address;
            request.Date = DateTime.UtcNow;
            this.Requests.Add(request);
        }
    }
}
