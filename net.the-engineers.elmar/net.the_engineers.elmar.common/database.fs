namespace net.the_engineers.elmar.common

module database =
    open System.Data
    open FsSql
    open Sql
    open System.Data.Common
    
    
//    type public Database =
//        static member CurrenctConnection
//            with get() = WcfContext.Current.Items.["CurrentDbConnection"] :?> IDbConnection
//            and set value =
//                WcfContext.Current.Items.["CurrentDbConnection"] <- value
//
//        static member Open (conn : IDbConnection)=
//            conn.Open()
//            Database.CurrenctConnection <- conn
//            conn

    let getValue<'T> (dataRecord : IDataRecord) column = 
        dataRecord.GetOrdinal column |> dataRecord.GetValue :?> 'T


    let P = Sql.Parameter.make

    type selectQueryObject<'T> =
        {
            query : string;
            parameters : Sql.Parameter list;
            deserialisation : (IDataRecord -> 'T) option;
        }

    type changeQueryObject =
        {
            query : string;
            parameters : Sql.Parameter list;
        }

    type queryPart =
        {
            where : string;
            parameter : Sql.Parameter option
        }

    let emptyQueryPart deserial = {query = ""; parameters = []; deserialisation = deserial}

    let w<'a when 'a : (new : unit -> 'a) and 'a : struct and 'a :> System.ValueType> column parametername operator (value : System.Nullable<'a>) =
        match value.HasValue with  
            | true -> { where = sprintf "%s %s %s" column operator parametername; parameter = Some <| P(parametername, value.Value)}
            | false -> {where = ""; parameter = Option<Sql.Parameter>.None}

    let combineAnd<'T> (x : queryPart) (qo : selectQueryObject<'T>) =
        match (x.parameter, qo.query) with
        | (Some xpart, "") -> { query = x.where; parameters = [x.parameter.Value]; deserialisation = qo.deserialisation}
        | (Some xpart, _) -> {query = qo.query + " AND " + x.where; parameters = x.parameter.Value :: qo.parameters; deserialisation = qo.deserialisation}
        | (None, _) -> { query = qo.query; parameters = qo.parameters; deserialisation = qo.deserialisation}

    let combineQueryParts<'T> (query :string) (qo :selectQueryObject<'T>) =
        let mutable q = query
        if (qo.query.Length > 0) then
            q <- query + " WHERE " + qo.query
        { query = q; parameters = qo.parameters; deserialisation = qo.deserialisation; }

    let executeReaderX<'T> (queryObj : selectQueryObject<'T>) (sql : SqlWrapper)  =
        sql.ExecReader queryObj.query queryObj.parameters
        |> Seq.ofDataReader
        |> Seq.map queryObj.deserialisation.Value

    let executeScalarX (queryObj : changeQueryObject) (connManager : Sql.ConnectionManager)=
        Sql.execScalar connManager queryObj.query queryObj.parameters