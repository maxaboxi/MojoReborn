export type SubscriptionDto = {
  id: string;
  moduleGuid: string;
  featureName: string;
  subscribedAt: string;
};

export type GetBlogSubscriptionsResponse = {
  subscriptions: SubscriptionDto[];
};

export type SubscribeToBlogRequest = {
  pageId: number;
};

export type SubscribeToBlogResponse = void;

export type UnsubscribeFromBlogRequest = {
  pageId: number;
  subscriptionId: string;
};

export type UnsubscribeFromBlogResponse = void;
