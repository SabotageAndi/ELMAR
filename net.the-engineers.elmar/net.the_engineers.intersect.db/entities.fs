namespace net.the_engineers.intersect

module entities =
    open System
    open net.the_engineers.elmar.everywhere.net45.Database
    
    type User()=
        inherit IdEntity()

        member val Username = "" with get,set
        member val Password = "" with get,set

    type Weather() =
        inherit IdEntity()

        member val LocationId = Guid.Empty with get,set
        member val DataFrom = DateTimeOffset.MinValue with get,set
        member val DataTo = DateTimeOffset.MinValue with get,set
        member val Temperature = 0.0m with get,set
        member val Humidity = 0.0m with get,set
        member val Rain = 0.0m with get,set
        member val WindSpeed = 0.0m with get,set
        member val WindDirection = 0.0m with get,set
        member val Clouds  = 0.0m with get,set
        member val Pressure  = 0.0m with get,set

    type PersonLocation() =
        inherit IdEntity()

        member val PersonId = Guid.Empty with get,set
        member val Timestamp = DateTimeOffset.MinValue with get,set
        member val Longitude = 0.0 with get,set
        member val Latitude = 0.0 with get,set
        member val Altitude = 0.0 with get,set

    type DataQueue() =
        inherit IdEntity()

        member val Type = "" with get,set
        member val Data = "" with get,set

    type MonitoringPerson() =
        inherit IdEntity()

        member val PersonId = Guid.Empty with get,set