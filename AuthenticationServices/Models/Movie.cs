using System;
using System.Collections.Generic;

namespace AuthenticationServices.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public int PostedByUser { get; set; }

    public string MovieName { get; set; } = null!;

    public string MovieThumnailImage { get; set; } = null!;

    public string MoviePoster { get; set; } = null!;

    public string? ListEpisode { get; set; }

    public int? TotalEpisodes { get; set; }

    public string Description { get; set; } = null!;

    public string ReleasedYear { get; set; } = null!;

    public string? AliasName { get; set; }

    public string? Director { get; set; }

    public string? MainCharacters { get; set; }

    public byte[]? Trailer { get; set; }

    public string? Comments { get; set; }

    public int? ViewByDate { get; set; }

    public int? ViewByWeek { get; set; }

    public int? ViewByMonth { get; set; }

    public int? ViewByYear { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Comment> CommentsNavigation { get; set; } = new List<Comment>();

    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

    public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();

    public virtual User PostedByUserNavigation { get; set; } = null!;
}
