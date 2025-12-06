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
  moduleId?: number;
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

export type GetPostsResponse = {
  isSuccess: boolean;
  message?: string;
  blogPosts: BlogPost[];
};

export type CategoryDto = {
  id: number;
  moduleId: number;
  categoryName: string;
};

export type GetCategoriesResponse = {
  isSuccess: boolean;
  message?: string;
  categories: CategoryDto[];
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

export type EditCommentRequest = {
  blogPostId: string;
  blogCommentId: string;
  title: string;
  content: string;
};

export type EditCommentResponse = {
  isSuccess: boolean;
  message: string;
};

export type DeleteCommentRequest = {
  blogPostId: string;
  blogCommentId: string;
};

export type DeleteCommentResponse = {
  isSuccess: boolean;
  message: string;
};
