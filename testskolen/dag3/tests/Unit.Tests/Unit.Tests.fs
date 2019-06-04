module Unit.Tests

open System
open Xunit

[<Fact>]
let ``One and one is two`` () =
   Assert.Equal(2, 1 + 1)

[<Fact>]
let ``Float numbers are equal with delta`` () = 
    Assert.Equal(1.0, 0.99, 1)

[<Fact>]
let ``Dates are equal within a time span`` () =
    let now = DateTime.Now
    let maybeLater = DateTime.Now
    Assert.Equal(now, maybeLater, TimeSpan.FromSeconds(1.0))

[<Theory>]
[<InlineData(1, 2, 3)>]
[<InlineData(2, 2, 4)>]
[<InlineData(1, 1, 2)>]
[<InlineData(23, 32, 55)>]
[<InlineData(100, 0, 100)>]
let ``Addition works as expected`` (x: int) (y: int) (expected: int) = 
   Assert.Equal(expected, x + y)


let data () = 
    [|
       [| "ab"; "c"; "abc"|]
       [| "ba"; "nan"; "banan"|]
    |]

[<Theory>]
[<MemberData("data")>]
let ``String concatenation works as expected`` (x: string) (y: string) (expected: string) = 
   Assert.Equal(expected, x + y)