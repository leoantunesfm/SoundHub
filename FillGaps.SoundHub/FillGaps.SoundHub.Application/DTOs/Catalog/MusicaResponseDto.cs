﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class MusicaResponseDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public int DuracaoSegundos { get; set; }
        public Guid AlbumId { get; set; }
        public string NomeArtista { get; set; }
    }
}
