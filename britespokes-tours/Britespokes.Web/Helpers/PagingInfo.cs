﻿using System;

namespace Britespokes.Web.Helpers
{
  public class PagingInfo
  {
    public int TotalItems { get; set; }
    public int PerPage { get; set; }
    public int CurrentPage { get; set; }

    public int TotalPages
    {
      get
      {
        return (int)Math.Ceiling((decimal)TotalItems / PerPage);
      }
    }
  }
}