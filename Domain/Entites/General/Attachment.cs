using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.General
{
    public class Attachment:BaseEntity
    {
        public string Table_Name { get; set; }
        public string Row_Id { get; set; }
        public string File_Type_Code { get; set; }
        public string File_Name { get; set; }
        public string MIME_Type { get; set; }
        public long File_Length { get; set; }
        public string File_Extension { get; set; }
        public string File_Path { get; set; }
    }
}
