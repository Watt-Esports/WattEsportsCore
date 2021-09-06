using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WattEsportsCore.Models;

namespace WattEsportsCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        public DbSet<Player> Players { get; set; }
        public DbSet<Valorant> Valorants { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<PreviousCommittee> PreviousCommittees { get; set; }
        public DbSet<Counterstrike> Counterstrike { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<LeagueOfLegends> LeagueOfLegends { get; set; }
        public DbSet<RainbowSix> RainbowSixs { get; set; }
        public DbSet<RocketLeague> RocketLeagues { get; set; }
        public DbSet<Hearthstone> Hearthstones { get; set; }


    }
}
