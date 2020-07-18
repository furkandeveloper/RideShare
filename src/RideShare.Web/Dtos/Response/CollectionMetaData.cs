using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Web.Dtos.Response
{
    public class CollectionMetaData
    {
        public CollectionMetaData()
        {
        }

        public CollectionMetaData(int count)
        {
            this.Count = count;
        }

        public int Count { get; set; }
    }
}
