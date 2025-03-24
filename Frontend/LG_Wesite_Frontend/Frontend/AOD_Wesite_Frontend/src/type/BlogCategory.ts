import { VideoType } from "./Video"

export type BlogCategoryType = {
    id: number,
    name: string,
    position: string,
    description: string,
    detail: string,
    styles: number,
    order: number,
    color: string,
    thumbnailUrl: string,
    imageUrl: string,
    features: string,
    videos: VideoType[], // Thay đổi từ 'video' thành 'videos' để khớp với List<Video>
    listBlogCategory: BlogCategoryType[], // Khớp với List<BlogCategoryModel>
}

export type AgileType = {
    listBlogCategory: BlogCategoryType[]
    imageUrl: string,
}

export type ActiveAccord = {
    index: number
    isActive: boolean,
}

export type ProductType = {
    listBlogCategory: BlogCategoryType[]
    videos: VideoType[], // Thay đổi từ 'video' thành 'videos' để khớp với List<Video>
}