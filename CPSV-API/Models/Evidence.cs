using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF;
using VDS.RDF.Query.Datasets;

namespace CPSV_API.Models
{
    public class Evidence
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string RelatedDocumentation { get; set; }

        public void createEvidence(SparqlResult results)
        {

            List<string> inputList = new List<string>();
            List<string> keywords = new List<string>();
            foreach (var item in results)
            {
                string temp = item.ToString();
                temp = temp.Replace("?field =", "");
                temp = temp.Replace("?description =", "");
                string[] temp2 = temp.Split(",");
                //below depending if the item has a keyword or not the code assigns the relative information to each corresponding field
                if (temp2[0].Contains("name")) {this.Name = temp2[1]; }
                if (temp2[1].Contains("PublicServices/id/doc/")) { this.Uri = temp2[1]; }
            }
           

        }
    }
}
