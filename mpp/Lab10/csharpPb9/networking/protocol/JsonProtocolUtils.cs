using networking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking.protocol
{
    internal class JsonProtocolUtils
    {
        public static Response createLoggedReposponse(Arbitru user)
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.LOGGED_ACC;
            response.user = DTOutils.getDTO(user);
            return response;
        }

        public static Response createErrorResponse(string errorMessage)
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.ERROR;
            response.errormessage = errorMessage;
            return response;
        }

        public static Response createRezultateResponse(Dictionary<Participant, long> punctajParticipant)
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.REZULTATE;
            response.punctaje = DTOutils.getDTO(punctajParticipant);
            return response;
        }

        public static Response createAllRezultateResponse(Dictionary<Participant, long> punctajParticipant)
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.ALL_REZULTATE;
            response.punctaje = DTOutils.getDTO(punctajParticipant);
            return response;
        }

        public static Response createParticipantiResponse(IEnumerable<Participant> participanti)
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.PARTICIPANTI;
            response.participanti = DTOutils.getDTO(participanti);
            return response;
        }

        public static Response createRezultatAddedResponse(Rezultat rezultat)
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.REZULTAT_ADDED;
            response.rezultat = DTOutils.getDTO(rezultat);
            return response;
        }

        public static Response createOkResponse()
        {
            Response response = new Response();
            response.typeCsharp = ResponseType.OK;
            return response;
        }

        public static Request createLoginRequest(Arbitru user)
        {
            Request request = new Request();
            request.typeCsharp = RequestType.LOGIN;
            request.user = DTOutils.getDTO(user);
            return request;
        }

        public static Request createAddRezultatRequest(Rezultat rezultat)
        {
            Request request = new Request();
            request.typeCsharp = RequestType.ADD_REZULTAT;
            request.rezultat = DTOutils.getDTO(rezultat);
            return request;
        }

        public static Request createGetParticipantiRequest()
        {
            Request request = new Request();
            request.typeCsharp = RequestType.GET_PARTICIPANTI;
            return request;
        }

        public static Request createGetRezultateRequest(Arbitru user)
        {
            Request request = new Request();
            request.user = DTOutils.getDTO(user);
            request.typeCsharp = RequestType.GET_REZULTATE;
            return request;
        }

        public static Request createGetAllRezultateRequest()
        {
            Request request = new Request();
            request.typeCsharp = RequestType.GET_ALL_REZULTATE;
            return request;
        }

        public static Request createLogoutRequest(Arbitru user)
        {
            Request request = new Request();
            request.typeCsharp = RequestType.LOGOUT;
            request.user = DTOutils.getDTO(user);
            return request;
        }
    }
}
