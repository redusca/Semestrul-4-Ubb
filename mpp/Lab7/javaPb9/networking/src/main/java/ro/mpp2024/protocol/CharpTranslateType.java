package ro.mpp2024.protocol;

import java.util.Map;

public class CharpTranslateType {

    private static Map<RequestType, Integer> requestTypeMap = Map.of(
        RequestType.LOGIN, 0,
        RequestType.LOGOUT, 1,
        RequestType.GET_REZULTATE, 2,
        RequestType.ADD_REZULTAT, 3,
        RequestType.GET_ALL_REZULTATE, 4,
        RequestType.GET_PARTICIPANTI, 5
    );

    private static Map<ResponseType, Integer> responseTypeMap = Map.of(
        ResponseType.OK, 0,
        ResponseType.ERROR, 1,
        ResponseType.REZULTATE, 2,
        ResponseType.ALL_REZULTATE, 3,
        ResponseType.LOGGED_ACC, 4,
        ResponseType.REZULTAT_ADDED, 5,
        ResponseType.PARTICIPANTI, 6
    );

    public static int toNumber(RequestType type){
        return requestTypeMap.get(type);
    }

    public static int toNumber(ResponseType type){
        return responseTypeMap.get(type);
    }
}
