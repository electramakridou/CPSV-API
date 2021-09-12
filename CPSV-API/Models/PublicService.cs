using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF;
using VDS.RDF.Query.Datasets;
using CPSV_API.Models;


namespace CPSV_API.Models
{
    public class PublicService
    {
       //the class follows the fields of CPSV-AP 
       public string Uri { get; set; }
       public string Name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> hasInput { get; set; }
        public string ProcessingTime { get; set; }
      

        // the below function receives the results from the SPARQL query and formats them accordingly to make them more easily readable
        public void createPublicService(SparqlResult result)
        {
             //string Name = null;
             //string Uri = null;

            
                
             string temp =result.ToString();
             temp = temp.Replace("?PS_uri =", "");
             temp = temp.Replace("?name =", "");
             string[] temp2 = temp.Split(",");
             
             if (temp2[0].Contains("PublicServices/id/ps/")) { this.Uri = temp2[0]; this.Name = temp2[1]; } else { this.Uri = temp2[1]; this.Name = temp2[0]; }       
            
        }

    public void createPublicService(SparqlResultSet results, string id)
    {
        this.Uri = "http://data.dai.uom.gr:8890/PublicServices/id/ps/" + id;
        List<string> inputList = new List<string>();
        List<string> keywords = new List<string>();
        foreach (var item in results)
        {
            // Each item is a property of CPSV-AP for a PS, by default the library returns the names of the variable used in the SPARQL query (here ?field,
            // ?decription) they are removed to make the results more easily readable by humans,  
            string temp = item.ToString();
            temp = temp.Replace("?field =", "");
            temp = temp.Replace("?description =", "");
            string[] temp2 = temp.Split(",");
            //below depending if the item has a keyword or not the code assigns the relative information to each corresponding field
            if (temp2[0].Contains("hasInput")) { inputList.Add(temp2[1]); }
            if (temp2[0].Contains("dcat#keyword")) { keywords.Add(temp2[1]); }
            if (temp2[0].Contains("processingTime")) { this.ProcessingTime = temp2[1]; }
            if (temp2[0].Contains("/terms/title")) { this.title = temp2[1]; }
            if (temp2[0].Contains("/terms/description")) { this.description = temp2[1]; }

        }
        if (inputList.Count == 0) { this.hasInput.Add("no values"); } else { this.hasInput = inputList; }
        if (keywords.Count == 0) { this.Keywords.Add("no values"); } else { this.Keywords = keywords; }



    }


}
}
