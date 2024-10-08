﻿using BarberProject.ViewModels.Comments;
using BarberProject.ViewModels.Employees;
using BarberProject.ViewModels.Services;
using BarberProject.ViewModels.SubServices;
using Domain.Models;

namespace BarberProject.ViewModels
{
    public class ServiceDetailPageVM
    {
        public List<Domain.Models.Service> Services { get; set; }
        public ServiceDetailVM Service { get; set; }
        public int Id { get; set; }
    }
}
