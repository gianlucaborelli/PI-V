﻿namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewCompanyRequest
    {

        public required string Name { get; set; }
        public List<string> Tags { get; set; } = [];
    }
}
