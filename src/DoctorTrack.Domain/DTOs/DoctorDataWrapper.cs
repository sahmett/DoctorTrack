namespace DoctorTrack.Domain.DTOs
{
    public class DoctorDataWrapper
    {
        public List<DoctorJson> data { get; set; }
    }
    public class DoctorJson
    {
        public string createdAt { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string hospitalName { get; set; }
        public int hospitalId { get; set; }
        public int specialtyId { get; set; }
        public double branchId { get; set; }
        public string nationality { get; set; }
        public string doctorId { get; set; }
    }

}