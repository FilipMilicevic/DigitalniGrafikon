﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CBO.Core.Orders.DataAccess.Csc.Entities
{
    public partial class DrivingOrder
    {
        public int DrivingOrderId { get; set; }
        public string RegionalCenter { get; set; }
        public DateTime? DrivingOrderDate { get; set; }
        public int? DrivingOrderTypeId { get; set; }
        public bool? Pickup { get; set; }
        public bool? Private { get; set; }
        public string FlightCode { get; set; }
        public string FlightName { get; set; }
        public int? OrderingPartnerId { get; set; }
        public string OrderingPartnerName { get; set; }
        public string BillingPartner { get; set; }
        public string TransferCode { get; set; }
        public string TransferMain { get; set; }
        public string TransferDeparture { get; set; }
        public string TransferDestination { get; set; }
        public int? PaxAdult { get; set; }
        public int? PaxChild { get; set; }
        public int? PaxInfant { get; set; }
        public int? PaxOutOfCost { get; set; }
        public int? PaxTotal { get; set; }
        public string PaxName { get; set; }
        public string PaxSurname { get; set; }
        public string DrivingOrderCode { get; set; }
        public int? DrivingOrderNumber { get; set; }
        public string DispatcherRemark { get; set; }
        public string DriverRemark { get; set; }
        public int? VehicleId { get; set; }
        public decimal? KilometersAmount { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpd { get; set; }
        public string LastUpdBy { get; set; }
        public string LastUpdApp { get; set; }
    }
}