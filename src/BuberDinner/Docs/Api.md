# Buber Dinner API

- [Buber Dinner API](#buber-dinner-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName": "Phuc",
    "lastName": "Nguyen",
    "email": "phuc.nguyen@devsoft.vn",
    "password": "123456"
}
```

#### Register Response

```js
200 OK
```

```json
{
    "id": "0b8557f9-52b3-4e99-8771-f67a136a18ce",
    "firstName": "Phuc",
    "lastName": "Nguyen",
    "email": "phuc.nguyen@devsoft.vn",
    "token": "eyJhb..z9dqcnXoY"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "email": "phuc.nguyen@devsoft.vn",
    "password": "123456"
}
```

#### Login Response

```js
200 OK
```

```json
{
    "id": "0b8557f9-52b3-4e99-8771-f67a136a18ce",
    "firstName": "Phuc",
    "lastName": "Nguyen",
    "email": "phuc.nguyen@devsoft.vn",
    "token": "eyJhb..z9dqcnXoY"
}
```