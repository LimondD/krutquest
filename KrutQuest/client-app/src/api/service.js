import axios from 'axios'
import store from '@/store'
import router from '@/router'

const getRequestHeaders = function () {
  var headers = {}

  var accessToken = store.getters.accessToken
  if (accessToken) {
    headers['Authorization'] = 'Bearer ' + accessToken
  }

  return headers
}

const doRequest = function (options) {
  if (options) {
    options.url = window.location.protocol + "//" + window.location.host + "/" + options.url    
  }

  return new Promise((resolve, reject) => {
    axios(options)
      .then(response => {
        if (response.data.success) {
          resolve(response.data.content)
        }
        else {
          reject(response.data.error)
        }
      })
      .catch(error => {
        if (error.response && error.response.status === 401) {
          router.push('/login')
        }
        
        reject(error)
      });
  })
}

const get = function (url) {
  var options = {
    method: 'GET',
    headers: getRequestHeaders(),
    url: url
  }

  return doRequest(options)
};

const post = function (url, data) {
  var options = {
    method: 'POST',    
    headers: getRequestHeaders(),
    url: url,
    data: data
  }
  
  return doRequest(options)
};

const upload = function (url, data) {
  var options = {
    method: 'POST',
    headers: getRequestHeaders(),
    url: url,
    data: data
  }

  options.headers['Content-Type'] = 'multipart/form-data'

  return doRequest(options)
};

export default {
  get: get,
  post: post,
  upload: upload
}
