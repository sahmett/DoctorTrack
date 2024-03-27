using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;

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

class Program
{
    static void Main(string[] args)
    {
        string jsonInput = @"
    {
        ""data"": [
    {
        ""createdAt"": ""2022-01-13T01:54:46.988Z"",
        ""name"": ""Mr. Ahmet Öz"",
        ""gender"": ""Male"",
        ""hospitalName"": ""Medicana Avcilar"",
        ""hospitalId"": 150,
        ""specialtyId"": 81036,
        ""branchId"": 29532.99,
        ""nationality"": ""TUR"",
        ""doctorId"": ""1""
    },
    {
        ""createdAt"": ""2022-04-29T02:25:52.521Z"",
        ""name"": ""Ahmet Pınar"",
        ""gender"": ""Male"",
        ""hospitalName"": ""Medicana Avcilar"",
        ""hospitalId"": 150,
        ""specialtyId"": 81036,
        ""branchId"": 29532.99,
        ""nationality"": ""TUR"",
        ""doctorId"": ""2""
    },
    {
        ""createdAt"": ""2021-12-29T20:34:25.337Z"",
        ""name"": ""Yasemin Öztürk"",
        ""gender"": ""Female"",
        ""hospitalName"": ""MedicalPark İzmir"",
        ""hospitalId"": 160,
        ""specialtyId"": 81036,
        ""branchId"": 45145.08,
        ""nationality"": ""TUR"",
        ""doctorId"": ""3""
    },
    {
        ""createdAt"": ""2022-04-30T04:05:06.158Z"",
        ""name"": ""Kübra Işık"",
        ""gender"": ""Female"",
        ""hospitalName"": ""MedicalPark Kadiköy"",
        ""hospitalId"": 160,
        ""specialtyId"": 18741,
        ""branchId"": 49875.59,
        ""nationality"": ""TUR"",
        ""doctorId"": ""4""
    },
    {
        ""createdAt"": ""2021-05-27T21:24:21.743Z"",
        ""name"": ""Aynur Aslan"",
        ""gender"": ""Female"",
        ""hospitalName"": ""Medicana Sisli"",
        ""hospitalId"": 150,
        ""specialtyId"": 20746,
        ""branchId"": 19747.48,
        ""nationality"": ""TUR"",
        ""doctorId"": ""5""
    },
    {
        ""createdAt"": ""2021-07-28T13:55:08.598Z"",
        ""name"": ""Elena Morissette"",
        ""gender"": ""Female"",
        ""hospitalName"": ""Memorial"",
        ""hospitalId"": 54892,
        ""specialtyId"": 88071,
        ""branchId"": 94982.39,
        ""nationality"": ""DE"",
        ""doctorId"": ""6""
    },
    {
        ""createdAt"": ""2021-06-14T18:01:30.325Z"",
        ""name"": ""Hamdi Öztürk"",
        ""gender"": ""Male"",
        ""hospitalName"": ""Medicana Sisli"",
        ""hospitalId"": 23701,
        ""specialtyId"": 9090,
        ""branchId"": 19747.48,
        ""nationality"": ""TUR"",
        ""doctorId"": ""7""
    },
    {
        ""createdAt"": ""2021-08-27T04:04:58.976Z"",
        ""name"": ""Craig O'Keefe"",
        ""gender"": ""Male"",
        ""hospitalName"": ""American Hospital"",
        ""hospitalId"": 58497,
        ""specialtyId"": 39708,
        ""branchId"": 46998.74,
        ""nationality"": ""USA"",
        ""doctorId"": ""8""
    },
    {
        ""createdAt"": ""2022-03-12T15:47:42.275Z"",
        ""name"": ""Aysun Çoşkun"",
        ""gender"": ""Female"",
        ""hospitalName"": ""Ege Hastanesi"",
        ""hospitalId"": 1058,
        ""specialtyId"": 82688,
        ""branchId"": 5663.64,
        ""nationality"": ""TUR"",
        ""doctorId"": ""9""
    },
    {
        ""createdAt"": ""2022-05-09T19:12:58.359Z"",
        ""name"": ""Cesar Batz"",
        ""gender"": ""Male"",
        ""hospitalName"": ""Ege Hastanesi"",
        ""hospitalId"": 1058,
        ""specialtyId"": 13798,
        ""branchId"": 5663.64,
        ""nationality"": ""ITA"",
        ""doctorId"": ""10""
    }
]
    }";

        // JSON verisini bir DoctorDataWrapper nesnesine dönüştür
        var doctorDataWrapper = JsonConvert.DeserializeObject<DoctorDataWrapper>(jsonInput);
        var doctors = doctorDataWrapper.data; // DoctorJson nesnelerinin listesini al

        // "data" dizisindeki her bir elemanı dolaş
        foreach (var doctor in doctors)
        {
            // Milleti "TUR" olan doktorları kontrol et
            if (doctor.nationality == "TUR")
            {
                // Cinsiyeti "Male" veya "Female" olarak güncelle
                doctor.gender = doctor.gender == "Erkek" ? "Male" : "Female";
            }
        }

        // Get the list separator for the current culture
        string listSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

        // Prepare the CSV content
        var csv = new StringBuilder();
        csv.AppendLine($"CreatedAt{listSeparator}Name{listSeparator}Gender{listSeparator}HospitalName{listSeparator}HospitalId{listSeparator}SpecialtyId{listSeparator}BranchId{listSeparator}Nationality{listSeparator}DoctorId");

        foreach (var doctor in doctors)
        {
            // Use the current culture's list separator
            var line = string.Format(CultureInfo.CurrentCulture,
                "\"{0}\"{9}\"{1}\"{9}\"{2}\"{9}\"{3}\"{9}{4}{9}{5}{9}{6}{9}\"{7}\"{9}\"{8}\"",
                doctor.createdAt,
                EscapeQuotes(doctor.name),
                doctor.gender,
                EscapeQuotes(doctor.hospitalName),
                doctor.hospitalId,
                doctor.specialtyId,
                doctor.branchId.ToString(CultureInfo.InvariantCulture),
                doctor.nationality,
                doctor.doctorId,
                listSeparator);

            csv.AppendLine(line);
        }

        // Write the CSV content to a file with UTF-8 encoding
        File.WriteAllText("doctors5.csv", csv.ToString(), Encoding.UTF8);
    }

    // Helper method to escape quotes within CSV fields
    static string EscapeQuotes(string input)
    {
        return input.Replace("\"", "\"\"");
    }
}
