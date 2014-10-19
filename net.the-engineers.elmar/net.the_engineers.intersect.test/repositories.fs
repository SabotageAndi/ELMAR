namespace net.the_engineers.intersect.test

open FsSql
open FsUnit
open System
open Xunit
open net.the_engineers.elmar.test.testinfrastructure
open net.the_engineers.intersect.entities
open net.the_engineers.intersect.repositories
open net.the_engineers.elmar.everywhere.requests


module dataqueue = 
    
    [<Fact>]
    let ``Get new entry for dataqueue``() =
        let dq = new DataQueue(Type = "type", Data="data")
        let queryObj = dataqueue.insert dq
        queryObj.parameters |> Seq.length |> should equal 2
        queryObj.parameters |> checkForSqlParameter "@type" dq.Type
        queryObj.parameters |> checkForSqlParameter "@data" dq.Data
        queryObj.query |> should equal "INSERT INTO \"intersect\".\"DataQueue\" (type, data) VALUES (@type, @data) RETURNING id;"


module weather =

    let fromPara = new DateTimeOffset(2014, 12, 3, 13, 37, 00, TimeSpan.FromHours(2.0))
    let toPara = new DateTimeOffset(2014, 12, 4, 13, 37, 00, TimeSpan.FromHours(2.0))
    let nullableFromPara = new Nullable<DateTimeOffset>(fromPara)
    let nullableToPara = new Nullable<DateTimeOffset>(toPara)
    let locationIdPara = new Nullable<Guid>(Guid.NewGuid())

    [<Fact>]
    let ``Check query without parameters``() =
        let fc = new WeatherSearchRequest()
        let queryObj = weather.search fc
        queryObj.parameters |> Seq.length |> should equal 0
        queryObj.query |> should equal "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\""

    [<Fact>]
    let ``Check query with From parameter``() =
        let fc = new WeatherSearchRequest()
        fc.From <- nullableFromPara
        let queryObj = weather.search fc
        queryObj.parameters |> Seq.length |> should equal 1
        queryObj.parameters |> checkForSqlParameter "@datafrom" fc.From

        queryObj.query |> should equal "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\" WHERE datafrom >= @datafrom"

    [<Fact>]
    let ``Check query with To parameter``() =
        let fc = new WeatherSearchRequest()
        fc.To <- nullableToPara
        let queryObj = weather.search fc
        queryObj.parameters |> Seq.length |> should equal 1
        queryObj.parameters |> checkForSqlParameter "@datato" fc.To

        queryObj.query |> should equal "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\" WHERE datato <= @datato"


    [<Fact>]
    let ``Check query with From and To parameter``() =
        let fc = new WeatherSearchRequest()
        fc.From <- nullableFromPara
        fc.To <- nullableToPara
        
        let queryObj = weather.search fc
        queryObj.parameters |> Seq.length |> should equal 2
        queryObj.parameters |> checkForSqlParameter "@datafrom" fc.From 
        queryObj.parameters |> checkForSqlParameter "@datato" fc.To 

        queryObj.query |> should equal "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\" WHERE datafrom >= @datafrom AND datato <= @datato"

    [<Fact>]
    let ``Check query with LocationId, From and To parameter``() =
        let fc = new WeatherSearchRequest()
        fc.LocationId <- locationIdPara
        fc.From <- nullableFromPara
        fc.To <- nullableToPara
        
        let queryObj = weather.search fc
        queryObj.parameters |> Seq.length |> should equal 3
        queryObj.parameters |> checkForSqlParameter "@datafrom" fc.From 
        queryObj.parameters |> checkForSqlParameter "@datato" fc.To 
        queryObj.parameters |> checkForSqlParameter "@locationid" fc.LocationId

        queryObj.query |> should equal "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\" WHERE locationid = @locationid AND datafrom >= @datafrom AND datato <= @datato"

    [<Fact>]
    let ``Check query with DateTime parameter``() =
        let fc = new WeatherSearchRequest()
        fc.DateTime <- nullableToPara
        let queryObj = weather.search fc
        queryObj.parameters |> Seq.length |> should equal 2
        queryObj.parameters |> checkForSqlParameter "@datafrom" fc.DateTime
        queryObj.parameters |> checkForSqlParameter "@datato" fc.DateTime

        queryObj.query |> should equal "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\" WHERE datafrom >= @datafrom AND datato <= @datato"

    [<Fact>]
    let ``Insert new Weather entry``() =
        let newWeather = new Weather()
        newWeather.Id <- 0
        newWeather.Clouds <- 1m
        newWeather.DataFrom <- fromPara
        newWeather.DataTo <- toPara
        newWeather.Humidity <- 1m
        newWeather.LocationId <- Guid.NewGuid()
        newWeather.Pressure <- 1m
        newWeather.Rain <- 1m
        newWeather.Temperature <- 1m
        newWeather.WindDirection <- 1m
        newWeather.WindSpeed <-1.0m

        let queryObj = weather.save newWeather
        queryObj.parameters |> Seq.length |> should equal 10
        queryObj.parameters |> checkForSqlParameter "@clouds" newWeather.Clouds
        queryObj.parameters |> checkForSqlParameter "@datafrom" newWeather.DataFrom
        queryObj.parameters |> checkForSqlParameter "@datato" newWeather.DataTo
        queryObj.parameters |> checkForSqlParameter "@humidity" newWeather.Humidity
        queryObj.parameters |> checkForSqlParameter "@locationid" newWeather.LocationId
        queryObj.parameters |> checkForSqlParameter "@pressure" newWeather.Pressure
        queryObj.parameters |> checkForSqlParameter "@rain" newWeather.Rain
        queryObj.parameters |> checkForSqlParameter "@temperature" newWeather.Temperature
        queryObj.parameters |> checkForSqlParameter "@winddirection" newWeather.WindDirection
        queryObj.parameters |> checkForSqlParameter "@windspeed" newWeather.WindSpeed
        queryObj.query |> should equal "INSERT INTO \"intersect\".\"Weather\"(
                                    locationid, datafrom, datato, temperature, humidity, rain, 
                                    windspeed, winddirection, clouds, pressure)
                            VALUES (@locationid, @datafrom, @datato, @temperature, @humidity, @rain, 
                                    @windspeed, @winddirection, @clouds, @pressure);
                            RETURNING id;
                        "

    [<Fact>]
    let ``Update Weather entry``() =
        let newWeather = new Weather()
        newWeather.Id <- 1
        newWeather.Clouds <- 1m
        newWeather.DataFrom <- fromPara
        newWeather.DataTo <- toPara
        newWeather.Humidity <- 1m
        newWeather.LocationId <- Guid.NewGuid()
        newWeather.Pressure <- 1m
        newWeather.Rain <- 1m
        newWeather.Temperature <- 1m
        newWeather.WindDirection <- 1m
        newWeather.WindSpeed <-1.0m

        let queryObj = weather.save newWeather
        queryObj.parameters |> Seq.length |> should equal 11
        queryObj.parameters |> checkForSqlParameter "@id" newWeather.Id
        queryObj.parameters |> checkForSqlParameter "@clouds" newWeather.Clouds
        queryObj.parameters |> checkForSqlParameter "@datafrom" newWeather.DataFrom
        queryObj.parameters |> checkForSqlParameter "@datato" newWeather.DataTo
        queryObj.parameters |> checkForSqlParameter "@humidity" newWeather.Humidity
        queryObj.parameters |> checkForSqlParameter "@locationid" newWeather.LocationId
        queryObj.parameters |> checkForSqlParameter "@pressure" newWeather.Pressure
        queryObj.parameters |> checkForSqlParameter "@rain" newWeather.Rain
        queryObj.parameters |> checkForSqlParameter "@temperature" newWeather.Temperature
        queryObj.parameters |> checkForSqlParameter "@winddirection" newWeather.WindDirection
        queryObj.parameters |> checkForSqlParameter "@windspeed" newWeather.WindSpeed
        queryObj.query |> should equal"UPDATE \"intersect\".\"Weather\"
                            SET locationid=@locationid, datafrom=@datafrom, datato=@datato, temperature=@temperature, humidity=@humidity, 
                                rain=@rain, windspeed=@windspeed, winddirection=@winddirection, clouds=@clouds, pressure=@pressure
                            WHERE id=@id;
                            SELECT @id;
                        "
    module personLocation = 

        let fromPara = new Nullable<DateTimeOffset>(new DateTimeOffset(2014, 8, 31, 13, 33, 37, TimeSpan.FromHours(1.0)))
        let toPara = new Nullable<DateTimeOffset>(new DateTimeOffset(2014, 8, 31, 13, 33, 37, TimeSpan.FromHours(1.0)))

        [<Fact>]
        let ``Search with From parameter ``() =
            let fc = new PersonLocationSearchDto()
            fc.From <- fromPara
            let queryObject = personlocation.search fc
            queryObject.parameters |> Seq.length |> should equal 1
            queryObject.parameters |> checkForSqlParameter "@from" fromPara
            queryObject.query |> should equal "SELECT id, personid, \"timestamp\", longitude, latitude, altitude FROM \"intersect\".\"PersonLocation\" WHERE \"timestamp\" >= @from"

        
        [<Fact>]
        let ``Search with To parameter ``() =
            let fc = new PersonLocationSearchDto()
            fc.To <- toPara
            let queryObject = personlocation.search fc
            queryObject.parameters |> Seq.length |> should equal 1
            queryObject.parameters |> checkForSqlParameter "@to" toPara
            queryObject.query |> should equal "SELECT id, personid, \"timestamp\", longitude, latitude, altitude FROM \"intersect\".\"PersonLocation\" WHERE \"timestamp\" <= @to"

        [<Fact>]
        let ``Search with To, From and PersonId parameter ``() =
            let fc = new PersonLocationSearchDto()
            fc.To <- toPara
            fc.From <- fromPara
            fc.PersonId <- new Nullable<Guid>(Guid.NewGuid())
            let queryObject = personlocation.search fc
            queryObject.parameters |> Seq.length |> should equal 3
            queryObject.parameters |> checkForSqlParameter "@to" fc.To
            queryObject.parameters |> checkForSqlParameter "@from" fc.From
            queryObject.parameters |> checkForSqlParameter "@personid" fc.PersonId
            queryObject.query |> should equal "SELECT id, personid, \"timestamp\", longitude, latitude, altitude FROM \"intersect\".\"PersonLocation\" WHERE \"timestamp\" >= @from AND \"timestamp\" <= @to AND personid = @personid"

        [<Fact>]
        let ``Save a new PersonLocation``() =
            let pl = new PersonLocation()
            pl.Id <- 0
            pl.PersonId <- Guid.NewGuid()
            pl.Altitude <- 1.0
            pl.Latitude <- 2.0
            pl.Longitude <- 3.0
            pl.Timestamp <- DateTimeOffset.Now
            let queryObject = personlocation.save pl
            queryObject.parameters |> Seq.length |> should equal 5
            queryObject.parameters |> checkForSqlParameter "@personid" pl.PersonId
            queryObject.parameters |> checkForSqlParameter "@timestamp" pl.Timestamp
            queryObject.parameters |> checkForSqlParameter "@altitude" pl.Altitude
            queryObject.parameters |> checkForSqlParameter "@longitude" pl.Longitude 
            queryObject.parameters |> checkForSqlParameter "@latitude" pl.Latitude
            queryObject.query |> should equal "INSERT INTO \"intersect\".\"PersonLocation\" (personid, \"timestamp\", longitude, latitude, altitude) VALUES (@personid, @timestamp, @longitude, @latitude, @altitude); RETURNING id;"

        [<Fact>]
        let ``Save a existing PersonLocation``() =
            let pl = new PersonLocation()
            pl.Id <- 1
            pl.PersonId <- Guid.NewGuid()
            pl.Altitude <- 1.0
            pl.Latitude <- 2.0
            pl.Longitude <- 3.0
            pl.Timestamp <- DateTimeOffset.Now
            let queryObject = personlocation.save pl
            queryObject.parameters |> Seq.length |> should equal 6
            queryObject.parameters |> checkForSqlParameter "@personid" pl.PersonId
            queryObject.parameters |> checkForSqlParameter "@timestamp" pl.Timestamp
            queryObject.parameters |> checkForSqlParameter "@altitude" pl.Altitude
            queryObject.parameters |> checkForSqlParameter "@longitude" pl.Longitude 
            queryObject.parameters |> checkForSqlParameter "@latitude" pl.Latitude
            queryObject.parameters |> checkForSqlParameter "@id" pl.Id
            queryObject.query |> should equal "UPDATE \"intersect\".\"PersonLocation\" SET personid=@personid, \"timestamp\"=@timestamp, longitude=@longitude, latitude=@latitude, altitude=@altitude WHERE id = @id;
                            SELECT @id;";

        [<Fact>]
        let ``Get PersonLocation By Id``() = 
            let id = 5
            let queryObject = personlocation.get id
            queryObject.parameters |> Seq.length |> should equal 1
            queryObject.parameters |> checkForSqlParameter "@id" id
            queryObject.query |> should equal "SELECT id, personid, \"timestamp\", longitude, latitude, altitude FROM \"intersect\".\"PersonLocation\" WHERE id = @id"

    module user = 
        [<Fact>]
        let ``Get User with Password``() =
            let username = "username"
            let password = "123456"
            let queryObject = user.getUserWithPassword username password
            queryObject.parameters |> Seq.length |> should equal 2
            queryObject.parameters |> checkForSqlParameter "@username" username
            queryObject.parameters |> checkForSqlParameter "@password" password
            queryObject.query |> should equal "SELECT id, username, password FROM \"intersect\".\"User\" WHERE username = @username AND password = @password"