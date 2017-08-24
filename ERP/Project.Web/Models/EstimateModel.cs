using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class EstimateModel
    {
        public long Estimate_ID { get; set; }

        public long Lead_ID_Fk { get; set; }

        public long Client_ID_Fk { get; set; }

        public string Date { get; set; }

        public Int64 Category { get; set; }

        public Int32 Language { get; set; }

        public long AssignTo { get; set; }

        public string Requirment { get; set; }

        public HttpPostedFileBase[] UploadedDoc { get; set; }

        public List<Project.Entity.Estimate> Estimations { get; set; }

        public string Priority { get; set; }

        public string CategoryName { get; set; }

        public string LanguageName { get; set; }

        public string Assigne { get; set; }

        public string AssigBy { get; set; }

        public string Lead { get; set; }

        public string Client { get; set; }

        public string Status { get; set; }

        public List<EstimationComments> Comments { get; set; }

        public List<EstimationUpload> Uploads { get; set; }


        public EstimateModel()
        {
            Estimations = new List<Entity.Estimate>();
            Comments = new List<EstimationComments>();
            Uploads = new List<EstimationUpload>();
        }
    }

    public class EstimationUpload{
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string UploadedDate { get; set; }
    }

    public class EstimationComments
    {
        public long CommentID { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
        public long CommentByID { get; set; }
        public string CommentBy { get; set; }
        public List<CommentUpload> CommUploads { get; set; }

        public EstimationComments()
        {
            CommUploads = new List<CommentUpload>();
        }
    }

    public class CommentUpload{
        public string UploadFileName {get;set;}
        public long CommentID { get; set; }
    }
}