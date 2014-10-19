namespace net.the_engineers.intersect

module repositories = 
    open Npgsql
    open FsSql
    open Sql
    open System.Data
//    open System.Web.Configuration
    open net.the_engineers.elmar.everywhere.requests
    open net.the_engineers.elmar.everywhere.net45.Database
    open net.the_engineers.intersect.entities
    open net.the_engineers.elmar.common.database
    
//    let private config = WebConfigurationManager.GetSection "Intersect" :?> DatabaseConfigurationSection

    let connectionString() = 
//        let config = WebConfigurationManager.GetSection "Elmar" :?> DatabaseConfigurationSection
        let csb = new NpgsqlConnectionStringBuilder()
//        csb.Database <- config.DatabaseName.Value
//        csb.Host <- config.DatabaseHost.Value
//        csb.UserName <- config.DatabaseUser.Value
//        csb.Password <- config.DatabasePassword.Value
        csb.ConnectionString
    
    let getConnection() = 
        let conn = new NpgsqlConnection(connectionString())
        conn :> IDbConnection
    
    let sqlWrapper connection = 
        let connMgr = connection |> Sql.withConnection
        SqlWrapper connMgr

    let connManager connection =
        connection |> Sql.withConnection
    
    let executeReader<'T> (queryObj : selectQueryObject<'T>) connection = connection |> sqlWrapper |> executeReaderX queryObj
    let executeScalar (queryObj : changeQueryObject) connection = connection |> connManager |> executeScalarX queryObj

        
    module dataqueue =
        let insert (dq : DataQueue) =
            { 
                query = "INSERT INTO \"intersect\".\"DataQueue\" (type, data) VALUES (@type, @data) RETURNING id;"; 
                parameters = [P("@type", dq.Type); P("@data", dq.Data)];
            }

    module weather =
        let asWeather (r: IDataRecord) =
            new Weather(Id = getValue r "id", LocationId = getValue r "LocationId", DataFrom = getValue r "DataFrom", 
                        DataTo = getValue r "DataTo", Temperature = getValue r "Temperature", 
                        Humidity = getValue r "Humidity", Rain = getValue r "Rain", WindSpeed = getValue r "WindSpeed", 
                        WindDirection = getValue r "Direction", Clouds = getValue r "Clouds", Pressure = getValue r "Pressure")
            
        let private insert (weather : Weather ) =
            {
                query = "INSERT INTO \"intersect\".\"Weather\"(
                                    locationid, datafrom, datato, temperature, humidity, rain, 
                                    windspeed, winddirection, clouds, pressure)
                            VALUES (@locationid, @datafrom, @datato, @temperature, @humidity, @rain, 
                                    @windspeed, @winddirection, @clouds, @pressure);
                        RETURNING id;
                        ";
                parameters = [
                                P("@locationid", weather.LocationId);
                                P("@datafrom", weather.DataFrom);
                                P("@datato", weather.DataTo);
                                P("@temperature", weather.Temperature);
                                P("@humidity", weather.Humidity);
                                P("@rain", weather.Rain);
                                P("@windspeed", weather.WindSpeed);
                                P("@winddirection", weather.WindDirection);
                                P("@clouds", weather.Clouds);
                                P("@pressure", weather.Pressure);
                            ]
            }

        let private update (weather : Weather ) =
            {
                query = "UPDATE \"intersect\".\"Weather\"
                        SET locationid=@locationid, datafrom=@datafrom, datato=@datato, temperature=@temperature, humidity=@humidity, 
                            rain=@rain, windspeed=@windspeed, winddirection=@winddirection, clouds=@clouds, pressure=@pressure
                        WHERE id=@id;
                        SELECT @id;
                        ";
                parameters =  [
                                P("@id", weather.Id);
                                P("@locationid", weather.LocationId);
                                P("@datafrom", weather.DataFrom);
                                P("@datato", weather.DataTo);
                                P("@temperature", weather.Temperature);
                                P("@humidity", weather.Humidity);
                                P("@rain", weather.Rain);
                                P("@windspeed", weather.WindSpeed);
                                P("@winddirection", weather.WindDirection);
                                P("@clouds", weather.Clouds);
                                P("@pressure", weather.Pressure);
                            ]
            }

        let save (weather : Weather) =
            match weather.Id with 
            | 0 -> insert weather
            | _ -> update weather

        let search (fc : WeatherSearchRequest) =
            let query = "SELECT id, locationid, datafrom, datato, temperature, humidity, rain, windspeed, winddirection, clouds, pressure FROM \"intersect\".\"Weather\""

            let locationIdPara = w "locationid" "@locationid" "=" fc.LocationId
            let fomPara = w "datafrom" "@datafrom" ">=" fc.From 
            let toPara = w "datato" "@datato" "<=" fc.To 
            let datetimeFromPara = w "datafrom" "@datafrom" ">=" fc.DateTime 
            let datetimeToPara = w "datato" "@datato" "<=" fc.DateTime 

            Some asWeather 
            |> emptyQueryPart 
            |> combineAnd locationIdPara
            |> combineAnd fomPara
            |> combineAnd toPara
            |> combineAnd datetimeFromPara
            |> combineAnd datetimeToPara
            |> combineQueryParts query 
            
    module personlocation =
        let asPersonLocation (pl : IDataRecord) =
            new PersonLocation(Id = getValue pl "Id", PersonId = getValue pl "PersonId", Timestamp = getValue pl "Timestamp", Longitude = getValue pl "Longitude", 
                            Latitude = getValue pl "Latitude", Altitude = getValue pl "Altitude")

        let search (fc: PersonLocationSearchDto) =
            let query = "SELECT id, personid, \"timestamp\", longitude, latitude, altitude FROM \"intersect\".\"PersonLocation\""
            
            let fromPara = w "\"timestamp\"" "@from" ">=" fc.From
            let toPara =  w "\"timestamp\"" "@to" "<=" fc.To
            let personIdPara = w "personid" "@personid" "=" fc.PersonId
            
            Some asPersonLocation
            |> emptyQueryPart
            |> combineAnd fromPara
            |> combineAnd toPara
            |> combineAnd personIdPara 
            |> combineQueryParts query

        let private insert (personLocation : PersonLocation) =
            {
                query = "INSERT INTO \"intersect\".\"PersonLocation\" (personid, \"timestamp\", longitude, latitude, altitude) VALUES (@personid, @timestamp, @longitude, @latitude, @altitude); RETURNING id;";
                parameters = [
                                P ("@personid", personLocation.PersonId);
                                P ("@timestamp", personLocation.Timestamp);
                                P ("@longitude", personLocation.Longitude);
                                P ("@latitude", personLocation.Latitude);
                                P ("@altitude", personLocation.Altitude);
                            ];
            }

        let private update (personLocation : PersonLocation) =
            {
                query = "UPDATE \"intersect\".\"PersonLocation\" SET personid=@personid, \"timestamp\"=@timestamp, longitude=@longitude, latitude=@latitude, altitude=@altitude WHERE id = @id;
                        SELECT @id;";
                parameters = [
                                P ("@personid", personLocation.PersonId);
                                P ("@timestamp", personLocation.Timestamp);
                                P ("@longitude", personLocation.Longitude);
                                P ("@latitude", personLocation.Latitude);
                                P ("@altitude", personLocation.Altitude);
                                P ("@id", personLocation.Id);
                            ];
            }
        
        let save (personLocation : PersonLocation) =
            match personLocation.Id with
            | 0 -> insert personLocation
            | _ -> update personLocation

        let get (id : int) =
            {
                query = "SELECT id, personid, \"timestamp\", longitude, latitude, altitude FROM \"intersect\".\"PersonLocation\" WHERE id = @id";
                parameters = [P("@id", id)];
                deserialisation = Some asPersonLocation;
            }
                
    module user =
        let asUser(u : IDataRecord) =
            new User(Id = getValue u "Id", Username = getValue u "Username", Password = getValue u "Password")

        let getUserWithPassword user password =
            {
                query = "SELECT id, username, password FROM \"intersect\".\"User\" WHERE username = @username AND password = @password";
                parameters = [P("@username", user); P("@password", password)];
                deserialisation = Some asUser;
            }     