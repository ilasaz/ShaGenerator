using System;
using System.Collections.Generic;
using System.Text;

namespace ShaGenerator.Contracts.Hashes;

public class HashesByDateResponse
{
    public List<HashDto> Hashes { get; set; }
}

public class HashDto
{
    public string Date { get; set; }
    public long Count { get; set; }
}
