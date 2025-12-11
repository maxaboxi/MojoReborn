export type BlogComment = {
  id: string;
  author: string;
  userGuid?: string | null;
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
  pageId: number;
  blogPostId: string;
  title: string;
  subTitle: string;
  content: string;
  categories: Category[];
};

export type DeletePostRequest = {
  pageId: number;
  blogPostId: string;
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

export type CategoryMutationResponse = {
  isSuccess: boolean;
  message?: string;
  isNotFound?: boolean;
  isNotAuthorized?: boolean;
};

export type CreateCategoryRequest = {
  pageId: number;
  categoryName: string;
};

export type EditCategoryRequest = {
  pageId: number;
  categoryId: number;
  categoryName: string;
};

export type DeleteCategoryRequest = {
  pageId: number;
  categoryId: number;
};

export type DeletePostResponse = {
  isSuccess: boolean;
  message: string;
};

export type CreateCommentRequest = {
  pageId: number;
  blogPostId: string;
  author: string;
  title: string;
  content: string;
};

export type CreateCommentResponse = {
  isSuccess: boolean;
  commentId: string;
  message: string;
};

export type EditCommentRequest = {
  pageId: number;
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
  pageId: number;
  blogPostId: string;
  blogCommentId: string;
};

export type DeleteCommentResponse = {
  isSuccess: boolean;
  message: string;
};
