export type BlogComment = {
  id: string;
  author: string;
  title?: string;
  content: string;
  createdAt: string;
};

export type Category = {
  id: number;
  categoryName: string;
};

export type BlogPost = {
  blogPostGuid: string;
  title: string;
  subTitle: string;
  content: string;
  author: string;
  createdAt: string;
  categories: string[];
  commentCount: number;
  comments: BlogComment[];
};

export type CreatePostCategoryDto = {
  id: number;
  categoryName: string;
};

export type CreatePostRequest = {
  pageId: number;
  author: string;
  title: string;
  subTitle: string;
  content: string;
  categories: CreatePostCategoryDto[];
};

export type CreatePostResponse = {
  isSuccess: boolean;
  blogPostId: string;
  message: string;
};

export type EditPostRequest = {
  blogPostId: string;
  title: string;
  subTitle: string;
  content: string;
  categories: Category[];
};

export type EditPostResponse = {
  isSuccess: boolean;
  blogPostId: string;
  message: string;
};

export type GetCategoriesResponse = {
  id: number;
  moduleId: number;
  categoryName: string;
};

export type DeletePostResponse = {
  isSuccess: boolean;
  message: string;
};

export type CreateCommentRequest = {
  pageId: number;
  blogPostId: string;
  userId: string;
  userName: string;
  userEmail: string;
  title: string;
  content: string;
};

export type CreateCommentResponse = {
  isSuccess: boolean;
  commentId: string;
  message: string;
};
