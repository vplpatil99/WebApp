using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class CourierMaster
{
    public int CourierId { get; set; }

    public string? Name { get; set; }

    public string? PersonIncharge { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? CourierZone { get; set; }

    public string? Area { get; set; }

    public string? ZipCode { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? Active { get; set; }
}
