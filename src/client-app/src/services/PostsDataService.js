import http from "../http-common";

class PostsDataService {
  getAll() {
    return http.get("/posts");
  }

  get(id) {
    return http.get(`/posts/${id}`);
  }

  create(data) {
    return http.post("/posts", data);
  }

  update(id, data) {
    return http.put(`/posts/${id}`, data);
  }

  delete(id) {
    return http.delete(`/posts/${id}`);
  }

  deleteAll() {
    return http.delete(`/posts`);
  }

  findByTitle(title) {
    return http.get(`/posts?title=${title}`);
  }
}

export default new PostsDataService();
