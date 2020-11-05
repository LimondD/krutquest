import service from './service'

const test = function () {
  return service.get('api/common/test');
};

const getContactInfo = function () {
  return service.get('api/common/getContactInfo')
};

const setContactInfo = function (contactInfo) {
  return service.post('api/common/setContactInfo', { contactInfo: contactInfo })
}

const whoAmI = function () {
  return service.get('api/common/whoAmI')
}

export default {
  test: test,
  getContactInfo: getContactInfo,
  setContactInfo: setContactInfo,
  whoAmI: whoAmI
}
