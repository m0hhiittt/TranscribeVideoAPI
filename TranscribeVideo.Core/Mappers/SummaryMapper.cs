using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Summary;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Mappers
{
    public static class SummaryMapper
    {
        public static Summaries ToEntity(CreateSummaryDto dto)
        {
            return new Summaries
            {
                Id = dto.TranscriptId,
                TranscriptId = dto.TranscriptId,
                SummaryText = dto.SummaryText,
                GeneratedBy = dto.GeneratedBy,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static SummaryResponseDto ToDto(Summaries summary)
        {
            return new SummaryResponseDto
            {
                Id = summary.Id,
                TranscriptId = summary.TranscriptId,
                SummaryText = summary.SummaryText,
                GeneratedBy = summary.GeneratedBy,
                CreatedAt = summary.CreatedAt
            };
        }
    }
}
