using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Core.BoardAggregate;

namespace TodoList.Infrastructure.Data.Config.Board;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder
            .HasOne(c => c.Details)
            .WithOne(cd => cd.Card)
            .HasForeignKey<CardDetails>(cd => cd.CardId);
    }
}
