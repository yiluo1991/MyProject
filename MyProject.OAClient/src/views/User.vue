<template>
    <div> 用户管理</div>
</template>
<script>
import axios from 'axios'
export default {
    mounted () {
        axios.get('/api/user/getlist').then(res=>{
            console.log(res.data)
        })
    }
}
</script>