using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity.Map
{
    public class OrganizationMap
    {
        public static void Configure(EntityTypeBuilder<Organization> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Organization");
        }
    }

}
