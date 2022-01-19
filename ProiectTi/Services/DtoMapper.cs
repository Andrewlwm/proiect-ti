using Microsoft.AspNetCore.Http;
using ProiectTi.Data.Models;
using ProiectTi.Models;
using ProiectTi.Services.Abstractions;
using System;
using System.IO;

namespace ProiectTi.Services
{
    public class DtoMapper : IDtoMapper
    {
        public Employee DtoToEmployee(EmployeeDto dto)
        {
            var employee = new Employee
            {
                Id = dto.Id,
                Nume = dto.Nume,
                Prenume = dto.Prenume,
                SalarBaza = dto.SalarBaza,
                Spor = dto.Spor,
                PremiiBrute = dto.PremiiBrute,
                BrutImpozitabil = dto.BrutImpozitabil,
                Impozit = dto.Impozit,
                CAS = dto.CAS,
                CASS = dto.CASS,
                Retineri = dto.Retineri,
                ViratCard = dto.ViratCard
            };

            if (dto.Picture is not null)
            {
                employee.DisplayPicture = new DisplayPicture
                {
                    ContentType = dto.Picture.ContentType,
                    Base64Image = Convert.ToBase64String(dto.Picture.Content)
                };
            }
            return employee;
        }

        public EmployeeDto EmployeeToDto(Employee employee)
        {
            var dto = new EmployeeDto
            {
                Nume = employee.Nume,
                Prenume = employee.Prenume,
                SalarBaza = employee.SalarBaza,
                Spor = employee.Spor ?? 0,
                PremiiBrute = employee.PremiiBrute ?? 0,
                BrutImpozitabil = employee.BrutImpozitabil,
                Impozit = employee.Impozit,
                CAS = employee.CAS,
                CASS = employee.CASS,
                Retineri = employee.Retineri ?? 0,
                ViratCard = employee.ViratCard
            };

            if (employee.DisplayPicture is not null)
            {
                dto.Picture = new EmployeePicture
                {
                    Content = Convert.FromBase64String(employee.DisplayPicture.Base64Image),
                    ContentType = employee.DisplayPicture.ContentType
                };
            }

            if (employee.SubmitPicture is not null)
            {
                dto.Picture = new EmployeePicture
                {
                    ContentType = employee.SubmitPicture.ContentType,
                    Content = GetBytes(employee.SubmitPicture)
                };
            }

            if (employee?.Id is not null)
            {
                dto.Id = (int)employee.Id;
            }

            return dto;
        }

        private byte[] GetBytes(IFormFile submitPicture)
        {
            using var stream = new MemoryStream();
            submitPicture.CopyTo(stream);
            return stream.ToArray();
        }
    }
}