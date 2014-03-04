using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Orders;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Britespokes.Services.Admin.GiftOrders
{
    public class GiftOrderReportService : IGiftOrderReportService
    {
        private readonly IGiftOrderQueryService _giftorderQueryService;
        private readonly CsvConfiguration _configuration;

        public GiftOrderReportService(IGiftOrderQueryService orderQueryService)
        {
            _giftorderQueryService = orderQueryService;
        }

    }
}