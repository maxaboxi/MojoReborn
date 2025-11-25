export type BlogComment = {
  id: string;
  author: string;
  content: string;
  createdAt: string;
}

export type BlogPost = {
  blogPostGuid: string;
  title: string;
  content: string;
  author: string;
  createdAt: string;
  categories: string[];
  commentCount: number;
  comments: BlogComment[];
}
