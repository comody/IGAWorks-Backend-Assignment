# IGAWorks Backend Assignment 
## Development Enviroment 
언어 및 프레임워크 : C# .net core    
개발툴 : Visual Studio MSDN Community   
소스코드 형상관리: Github   
서버 형상관리: Docker   
클라우드 환경 : AWS   
클라우드 서비스 : EC2, SQS, Lambda, RDS

## Project Structure
### Event Collect API 
클라이언트로부터 event를 수신합니다.   
SQS로 event 내용을 전송합니다.
### Event Collect Worker(Lambda)
SQS로 전달받은 event를 RDS DB에 입력합니다.
### Event Search API
RDS DB에 조회하여 결과를 반환합니다.

### Event Bot
유저 상품구매 프로세스를 재현 및 로깅해주는 콘솔 어플리케이션 입니다.


## API SPEC
BaseURL: http://54.180.98.97:80/

### collect API
```
POST /api/collect
HTTP/1.1
Content-Type: application/json
 
{
    "event_id" : "RANDOM_EVENT_GUID",
    "user_id" : "RANDOM_USER_GUID",
    "event" : "READY_PURCHASE",
    "parameters" :
    {
        "amount": 1,
        "product_id": "PRODUCT_ID_GUID",
        "price": 888,
        "left_stock": 32,
        "total_price": 888,
        "left_point": 2750
    }
}
 
=>
 
{
    "is_success" : "true"
}
```

### search API
```
POST /api/search
HTTP/1.1
Content-Type: application/json
 
{
    "user_id" : "RANDOM_USER_GUID"
}
 
=>
 
{
    "is_success" : "true",
    "results" : [
        {
            "event_id": "RANDOM_EVENT_GUID",
            "event": "READY_PURCHASE",
            "parameters": {
                "amount": 4,
                "product_id": "PRODUCT_ID_GUID",
                "price": 1583,
                "left_stock": 32,
                "total_price": 6332,
                "left_point": 181
            },
            "event_datetime": "2022-03-30T19:00:39.912Z"
        }
    ]
}
```
## Build & Deploy
```
$ dotnet publish -c Release -o published   
$ aws ecr get-login-password --region ap-northeast-2 | docker login --username AWS --password-stdin [574066197459.dkr.ecr.ap-northeast-2.amazonaws.com](http://574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/)   
$ docker build -t dfinery-backend-assignment .    
$ docker tag dfinery-backend-assignment:latest [574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/dfinery-backend-assignment:latest](http://574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/dfinery-backend-assignment:latest)    
$ docker push [574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/dfinery-backend-assignment:latest](http://574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/dfinery-backend-assignment:latest)    
    
$ aws ecr get-login-password --region ap-northeast-2 | docker login --username AWS --password-stdin [574066197459.dkr.ecr.ap-northeast-2.amazonaws.com](http://574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/)    
$ docker pull [574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/dfinery-backend-assignment:latest](http://574066197459.dkr.ecr.ap-northeast-2.amazonaws.com/dfinery-backend-assignment:latest)    
$ docker run -d -p 80:80 3b2e0d1b0d1b    
```