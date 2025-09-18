namespace MasLazu.AspNet.Authentication.Core.Abstraction.Models;

public record CreateTimezoneRequest(
    string Identifier,
    string Name,
    int OffsetMinutes
);
