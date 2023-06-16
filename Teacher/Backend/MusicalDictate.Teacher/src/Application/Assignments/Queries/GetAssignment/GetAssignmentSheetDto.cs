﻿using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignment;

public class GetAssignmentSheetDto : IMapFrom<Sheet>
{
    public string Id { get; set; }
    public string MusicXml { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<Sheet, GetAssignmentSheetDto>()
        .ForMember(r => r.MusicXml, opt => opt.MapFrom(s => Convert.ToBase64String(s.MusicXml)));
}