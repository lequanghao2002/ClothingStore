﻿namespace ClothingStore.Models.Authorize
{
    public class AuthorizeDTO
    {
        public int ID_Authorize { get; set; }
        public string? Authorize { get; set; }
    }

    public class AuthorizeNoIdDTO
    {
        public string? Authorize { get; set; }
    }
}
