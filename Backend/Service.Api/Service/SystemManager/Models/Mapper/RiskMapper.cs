using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Risks;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class RiskMapper
    {
        public static RiskDto ToDto(this Risk risk)
        {
            return risk switch
            {
                TermoRisk termo => new TermoRiskDto
                {
                    Id = termo.Id,
                    Category = termo.Category,
                    SubCategory = termo.SubCategory,
                    Activity = termo.Activity,
                    MetabolicRate = termo.MetabolicRate,
                    CreatedAt = termo.CreatedAt,
                    UpdatedAt = termo.UpdatedAt
                },

                // quando criar novos tipos, só adicionar mais cases:
                // RuidoRisk ruido => new RuidoRiskDto { ... },

                _ => throw new NotSupportedException($"Tipo de Risk não mapeado: {risk.GetType().Name}")
            };
        }

        public static List<RiskDto> ToDtoList(this List<Risk> risks)
        {
            return risks.Select(r => r.ToDto()).ToList();
        }

        public static Risk ToEntity(this RiskDto riskDto)
        {
            return riskDto switch
            {
                TermoRiskDto termoDto => new TermoRisk
                {
                    Id = termoDto.Id,
                    Category = termoDto.Category,
                    SubCategory = termoDto.SubCategory,
                    Activity = termoDto.Activity,
                    MetabolicRate = termoDto.MetabolicRate,
                    CreatedAt = termoDto.CreatedAt,
                    UpdatedAt = termoDto.UpdatedAt
                },
                // quando criar novos tipos, só adicionar mais cases:
                // RuidoRiskDto ruidoDto => new RuidoRisk { ... },
                _ => throw new NotSupportedException($"Tipo de RiskDto não mapeado: {riskDto.GetType().Name}")
            };
        }

        //public static List<Risk> ToEntityList(this List<RiskDto> riskDtos)
        //{
        //    return riskDtos.Select(dto => dto switch
        //    {
        //        TermoRiskDto termoDto => new TermoRisk
        //        {
        //            Id = termoDto.Id,
        //            Category = termoDto.Category,
        //            SubCategory = termoDto.SubCategory,
        //            Activity = termoDto.Activity,
        //            MetabolicRate = termoDto.MetabolicRate,
        //            CreatedAt = termoDto.CreatedAt,
        //            UpdatedAt = termoDto.UpdatedAt
        //        },
        //        // quando criar novos tipos, só adicionar mais cases:
        //        // RuidoRiskDto ruidoDto => new RuidoRisk { ... },
        //        _ => throw new NotSupportedException($"Tipo de RiskDto não mapeado: {dto.GetType().Name}")
        //    }).ToList();
        //}
    }
}
