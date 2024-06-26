﻿using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using DoctorTrack.Domain.Interfaces;
using DoctorTrack.Domain.DTOs;

namespace DoctorTrack.WebAPI.Services
{
    public class CsvExportService : ICsvExportService
    {
        private readonly HttpClient _httpClient;
        private const string FetchDoctorsEndpoint = "fetchDoctors";
        private const string CsvFilePath = "DoctorsOutput.csv";
        private const string _baseAddress = "https://a93ced42-c421-4f38-a0ee-25fc667483c0.mock.pstmn.io/";

        public CsvExportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ExportDoctorsToCsvAsync2()
        {
            
            var doctorDataJson = await _httpClient.GetStringAsync($"{_baseAddress}{FetchDoctorsEndpoint}");
            var doctorDataWrapper = JsonConvert.DeserializeObject<DoctorDataWrapperDto>(doctorDataJson);

            var csvContent = new StringBuilder();
            csvContent.AppendLine("CreatedAt,Name,Gender,HospitalName,HospitalId,SpecialtyId,BranchId,Nationality,DoctorId");

            foreach (var doctor in doctorDataWrapper.data)
            {
                if (doctor.nationality == "TUR")
                {
                    doctor.gender = doctor.gender == "Erkek" ? "Male" : "Female";
                }

                csvContent.AppendLine($"{doctor.createdAt},{doctor.name},{doctor.gender},{doctor.hospitalName},{doctor.hospitalId},{doctor.specialtyId},{doctor.branchId.ToString(CultureInfo.InvariantCulture)},{doctor.nationality},{doctor.doctorId}");
            }

            await File.WriteAllTextAsync(CsvFilePath, csvContent.ToString(), Encoding.UTF8);
        }

        public async Task ExportDoctorsToCsvAsync()
        {
            var doctorDataJson = await _httpClient.GetStringAsync($"{_baseAddress}{FetchDoctorsEndpoint}");
            var doctorDataWrapper = JsonConvert.DeserializeObject<DoctorDataWrapperDto>(doctorDataJson);

            
            string listSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            var csvContent = new StringBuilder();
            csvContent.AppendLine($"CreatedAt{listSeparator}Name{listSeparator}Gender{listSeparator}HospitalName{listSeparator}HospitalId{listSeparator}SpecialtyId{listSeparator}BranchId{listSeparator}Nationality{listSeparator}DoctorId");

            foreach (var doctor in doctorDataWrapper.data)
            {
                if (doctor.nationality == "TUR")
                {
                    doctor.gender = doctor.gender == "Male" ? "Erkek" : (doctor.gender == "Female" ? "Kadın" : doctor.gender);
                }


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

                csvContent.AppendLine(line);
            }

           
            await File.WriteAllTextAsync(CsvFilePath, csvContent.ToString(), Encoding.UTF8);
        }

       
        static string EscapeQuotes(string input)
        {
            return input.Replace("\"", "\"\"");
        }
    }
}