import service from './service'

const login = function (data) {
  return service.post('api/auth/login', data);
};

const loginAdmin = function (data) {
  return service.post('api/auth/loginAdmin', data);
};

export default {
  login: login,
  loginAdmin: loginAdmin
}
