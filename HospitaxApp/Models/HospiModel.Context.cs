﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitaxApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitaxDBEntities : DbContext
    {
        public HospitaxDBEntities()
            : base("name=HospitaxDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ankiety> Ankieties { get; set; }
        public virtual DbSet<Hospitujacy> Hospitujacies { get; set; }
        public virtual DbSet<OcenySlowne> OcenySlownes { get; set; }
        public virtual DbSet<Protokoly> Protokolies { get; set; }
        public virtual DbSet<Prowadzacy> Prowadzacies { get; set; }
    }
}
